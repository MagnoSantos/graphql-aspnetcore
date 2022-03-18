using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Domain.GraphQL.Customers.Mutations;
using GraphQL.Sample.Domain.GraphQL.Customers.Queries;
using HotChocolate.Data;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Sample.Infra.CrossCutting.IoC.Modules;

public static class ApplicationModule
{
    public static void Register(IServiceCollection services)
    {
        services.AddGraphQLServer()
                .InitializeOnStartup()
                .RegisterDbContext<ApplicationDbContext>(DbContextKind.Pooled)
                .AddQueryType(q => q.Name("Query"))
                    .AddQueries()
                .AddMutationType(m => m.Name("Mutation"))
                    .AddMutations();
    }

    public static IRequestExecutorBuilder AddQueries(this IRequestExecutorBuilder builder)
    {
        return builder.AddTypeExtension<CustomersQueries>();
    }

    public static IRequestExecutorBuilder AddMutations(this IRequestExecutorBuilder builder)
    {
        return builder.AddTypeExtension<CustomersMutations>();
    }
}