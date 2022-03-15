using GraphQL.Sample.Domain.GraphQL.Customers.Mutations;
using GraphQL.Sample.Domain.GraphQL.Customers.Queries;

namespace GraphQL.Sample.Api.Configurations;

public static class GraphQLConfiguration
{
    public static void ConfigureGraphQL(this IServiceCollection services)
         => services.AddGraphQLServer()
                .AddQueryType<CustomersQueries>()
                .AddMutationType<CustomersMutation>();
}