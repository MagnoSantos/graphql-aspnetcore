using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Data.Entities;
using GraphQL.Sample.Infra.Data.DataContext;
using GraphQL.Sample.Infra.Loaders;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Queries;

[ExtendObjectType("Query")]
public class CustomersQueries
{
    [UseApplicationDbContext]
    public Task<List<Customer>> GetCustomersAsync([ScopedService] ApplicationDbContext context) =>
        context.Customers.ToListAsync();

    public async Task<Customer> GetCustomerAsync(Guid id, EntityByIdBatchDataLoader<Customer> dataLoader, CancellationToken cancellationToken)
            => await dataLoader.LoadAsync(id, cancellationToken);
}