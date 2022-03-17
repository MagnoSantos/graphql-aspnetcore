using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Data.Entities;
using GraphQL.Sample.Domain.GraphQL.Attributes;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

public class CustomersMutations
{
    [UseApplicationDbContext]
    public async Task<CustomersPayload> AddCustomersAsync(
        CustomersInput input,
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

        return new CustomersPayload(customer);
    }
}