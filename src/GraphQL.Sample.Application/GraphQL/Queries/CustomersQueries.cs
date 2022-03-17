using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Data.Entities;
using GraphQL.Sample.Infra.Data.DataContext;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Queries;

[ExtendObjectType("Query")]
public class CustomersQueries
{
    [UseApplicationDbContext]
    public Task<List<Customer>> GetCustomers([ScopedService] ApplicationDbContext context) =>
        context.Customers.ToListAsync();
}