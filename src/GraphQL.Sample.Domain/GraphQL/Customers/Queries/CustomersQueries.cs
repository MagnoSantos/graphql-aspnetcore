using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Data.Entities;
using GraphQL.Sample.Domain.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Queries;

public class CustomersQueries
{
    [UseApplicationDbContext]
    public Task<List<Customer>> GetCustomers([ScopedService] ApplicationDbContext context) => 
        context.Customers.ToListAsync();
}