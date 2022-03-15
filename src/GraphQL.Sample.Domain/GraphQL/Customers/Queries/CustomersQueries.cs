using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Data.Entities;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Queries;

public class CustomersQueries
{
    public IQueryable<Customer> GetCustomers([Service] ApplicationDbContext context)
        => context.Customers;
}