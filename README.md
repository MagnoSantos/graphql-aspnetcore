# graphql-aspnetcore
<div id="top"></div>
<br />
  <h2 align="center">GraphQL API</h2>

  <p align="center">
    Repositório exemplo GraphQL utilizando ASP .NET Core 6.0 em construção GraphQL (ChilliCream GraphQL Plataform)
  </p>
</div>

## Sobre o projeto

Esse projeto visa a implementação simples do recurso de GraphQL para construção de queries e mutations. Para isso, será implementado um repositório template considerando o workshop do ChilliCream GraphQL Plataform). 

### Criado com

* [.NET](https://dotnet.microsoft.com/download/dotnet/6.0)
* [Entity Framework Core](https://docs.microsoft.com/pt-br/ef/core/)
* [HotChocolate](https://github.com/ChilliCream/hotchocolate)


## Pré-Requisitos

Necessário possuir o SDK do .NET Core 6.0:
* Disponível em: https://dotnet.microsoft.com/download/dotnet/6.0


## Como usar

Será necessário gerar as Migrations para representação do banco de dados SQLite, para isso digite o comando:

```dotnet ef migrations add InitialCreate```

Com isso o Entity Framework Core, criará um diretório chamado Migrations em seu projeto. Posteriormente, será necessário que o EF crie seu banco de dados e seu esquema a partir da migração. Isso pode ser feito por meio do seguinte comando: 

```dotnet ef database update```

Com isso, o aplicativo está pronto para ser executado no novo banco de dados.

## Estrutura do projeto 
O projeto está estruturado em camadas de abstração, sendo definidas pela hierarquia a seguir: 

```
$ tree
├── .config
├── GraphQL.Sample.Api                        
├── GraphQL.Sample.Domain
└── GraphQL.Sample.Data
.gitignore
README.md
GraphQL.Sample.sln
```

- GraphQL.Sample.Api - Projeto do tipo Web Application SDK 6.0
- GraphQL.Sample.Domain - Projeto do tipo Class Library SDK 6.0
- GraphQL.Sample.Data - Projeto do tipo Class Library SDK 6.0


## Queries and Mutations

Construção de queries e mutations para cadastro de clientes (utilizada base de dados In Memory do Entity Framework Core):

- Estrutura de *queries*:

```csharp
public class CustomersQueries
{
    [UseApplicationDbContext]
    public Task<List<Customer>> GetCustomers([ScopedService] ApplicationDbContext context) => 
        context.Customers.ToListAsync();
}
```

- Estrutura de *mutations*:
```csharp
    [UseApplicationDbContext]
    public async Task<AddCustomersPayload> AddCustomersAsync(
      AddCustomersInput input, 
      [ScopedService] ApplicationDbContext context
    )
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Cpf = input.Cpf,
            LastName = input.LastName
        };

        context.Customers.Add(customer);
        await context.SaveChangesAsync();

        return new AddCustomersPayload(customer);
    }
```

- Injeção de depedências:

No Startup métdo ConfigureServices coinfigurar o GraphQL informando as queries e mutations desenvolvidas:

```csharp
public static void ConfigureGraphQL(this IServiceCollection services)
         => services.AddGraphQLServer()
                .AddQueryType<CustomersQueries>()
                .AddMutationType<CustomersMutation>();
```

No método Configure, adicionar: 
```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});
```

Será possível analisar os schemas e realizar operações através do: ```<address>/graphql/```

## Controle de anuláveis

O sistema de tipos GraphQL distingue entre tipos anuláveis e não anuláveis. Isso ajuda o consumidor da API fornecendo garantias quando um valor de campo pode ser confiávl para nunca ser nulo ou quando a entreada não pode ser nula. Esse lado é interessante pois não necessita de escrever várias verificações nulas para coisas que nunca serão nulas. 

No projeto *GraphQL.Sample.Data* existe uma propriedade no .csproj sobre esse aspecto: 

```<Nullable>enable</Nullable>```

Com isso, deve-se informar ao input que os campos serão anuláveis:

```csharp
public record AddCustomersInput(Guid Id,
                                string? Name,
                                string? LastName,
                                string? Cpf);
```

Também na definição da entidade:

```csharp
public class Customer
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string? Name { get; set; }

    [StringLength(200)]
    public string? LastName { get; set; }

    [StringLength(14)]
    public string? Cpf { get; set; }
}
```

## Query Execution

O mecanismo de execução do GraphQL sempre tentará executar campos em paralelo para otimizar a busca de dados e reduzir o tempo de espera. 
O Entity Framwork terá, problema com isso, pois o ```DbContext``` não e thread-safe. 

Query de exemplo: 
```
query  GetCustomersInParallel {
   a : customer {
    name
  }
  b : customer {
    name 
  }
  c : customer {
    name 
  }
}
```

Essa consulta tenta buscar três vezes o cliente em paralelo, que usava o mesmo ```DbContext``` isso levará a uma exceção. 
Para resolver isso é possível utilizar o ```DbContext``` pooling, que nos permite emitir uma instância de ```DbContext```para cada campo que precisar
de uma. Porém ao invés de criar uma instância para cada campo e jogá-la fora depois de usá-la, estamos alugando para que os campos e solicitações 
possa reutilizá-la. 

1. Adicionar injeção como pooled:
```csharp
services.AddPooledDbContextFactory<ApplicationDbContext>(options => options.UseInMemoryDataBase("Sample Database"));
```

2. Criar uma *extensions* com a definição: 
```csharp
 public static class ObjectFieldDescriptorExtensions
    {
        public static IObjectFieldDescriptor UseDbContext<TDbContext>(
            this IObjectFieldDescriptor descriptor)
            where TDbContext : DbContext
        {
            return descriptor.UseScopedService<TDbContext>(
                create: s => s.GetRequiredService<IDbContextFactory<TDbContext>>().CreateDbContext(),
                disposeAsync: (s, c) => c.DisposeAsync());
        }
    }
```

O UseContext cria um novo middleware que trata o escopo de um campo. A parte create será alugada ao pooll para a DbContext, a parte dispose desvolverá após o 
término do processamento do middleware. Isso é tratado de forma transparente pelo IDbContextFactory<T>.

3. Criar atributo: 
```csharp
 public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<ApplicationDbContext>();
        }
    }
```

4. Por fim, utilizar em Queries e Mutations:
```csharp 
[UseApplicationDbContext]
public Task<List<Customer>> GetCustomers([ScopedService] ApplicationDbContext context) => 
    context.Customers.ToListAsync();
```

## Data Loader

Toda tecnologia de busca sofre problema n+1. A diferença é que o GraphQL o problema ocorre no sercidor e não no cliente. Podemos
portanto lidar com esse problema uma vez no servidor, e não em cada cliente. 

DataLoader é utilizado para agrupar as solicitações em uma chamada única ao banco de dados. 

```csharp
public class EntityByIdBatchDataLoader<TEntity> : BatchDataLoader<Guid, TEntity> where TEntity : BaseEntity
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public EntityByIdBatchDataLoader(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                         IBatchScheduler batchSchedule)
            : base(batchSchedule)
        {
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, TEntity>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            using ApplicationDbContext dbContext = await _dbContextFactory.CreateDbContextAsync();

            return await dbContext.Set<TEntity>()
                                  .Where(entity => keys.Contains(entity.Id))
                                  .ToDictionaryAsync(entity => entity.Id, cancellationToken);
        }
    }
``` 

Neste trecho centralizamos a busca de dados e reduzimos o número de idas e vinda para nossa base de dados. Em vez de buscar os dados de um repositório, buscamos
os dados do carregador de dados. O carregador agrupa todas as solicitações em uma única. 

## Paging

Exemplo de consulta SDL com filtros: 

```sdl
type UsersConnection {
  pageInfo: PageInfo!
  edges: [UsersEdge!]
  nodes: [User!]
}
```

Para utilizá-lo basta definir o middleware ```[UsePagging]```. Este, aplicará os argumentos de paginação ao que for retornado. 

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.