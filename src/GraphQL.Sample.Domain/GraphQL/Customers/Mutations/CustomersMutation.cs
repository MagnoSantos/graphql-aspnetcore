using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Data.Entities;
using GraphQL.Sample.Domain.GraphQL.Extensions;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

public class CustomersMutation
{
    [UseApplicationDbContext]
    public async Task<AddCustomersPayload> AddCustomersAsync(AddCustomersInput input, [ScopedService] ApplicationDbContext context)
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