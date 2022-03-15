using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Data.Entities;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

public class CustomersMutation
{
    public async Task<AddCustomersPayload> AddCustomersAsync(AddCustomersInput input, [Service] ApplicationDbContext context)
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

        return new AddCustomersPayload(customer);
    }
}