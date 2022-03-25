using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Data.Entities;
using GraphQL.Sample.Infra.Data.DataContext;
using GraphQL.Sample.Infra.Data.Repositories;
using GraphQL.Sample.Infra.Loaders;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Queries;

[ExtendObjectType("Query")]
public class CustomersQueries
{
    [UseApplicationDbContext]
    [UsePaging(MaxPageSize = 5, IncludeTotalCount = true)]
    [UseFiltering]
    public Task<List<Customer>> GetCustomersAsync([ScopedService] ApplicationDbContext context) =>
        context.Customers.ToListAsync();

    public async Task<Customer> GetCustomerByIdAsync(Guid id, EntityByIdBatchDataLoader<Customer> dataLoader, CancellationToken cancellationToken)
            => await dataLoader.LoadAsync(id, cancellationToken);

    public async Task<Customer> GetCustomerByNameAsync(string customerName, [Service] ICustomerRepository customerRepository)
        => await customerRepository.GetOneAsync(pred => pred.Name == customerName);
}