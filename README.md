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


<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.