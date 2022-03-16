using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Domain.GraphQL.Customers.Mutations;
using GraphQL.Sample.Domain.GraphQL.Customers.Queries;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace GraphQL.Sample.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class AppConfiguration
{
    public static void ConfigureGraphQL(this IServiceCollection services)
         => services.AddGraphQLServer()
                .AddQueryType<CustomersQueries>()
                .AddMutationType<CustomersMutation>();

    public static void ConfigureDataBase(this IServiceCollection services)
        => services.AddPooledDbContextFactory<ApplicationDbContext>(options => options.UseInMemoryDatabase(GetConnectionString()));

    private static string GetConnectionString() => "DataBase Sample";
}