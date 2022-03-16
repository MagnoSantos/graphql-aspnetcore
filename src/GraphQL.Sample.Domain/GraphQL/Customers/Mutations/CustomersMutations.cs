using GraphQL.Sample.Data.DataContext;
using GraphQL.Sample.Data.Entities;
using GraphQL.Sample.Domain.Abstractions.Common;
using GraphQL.Sample.Domain.GraphQL.Attributes;
using System.Collections.Immutable;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

public class CustomersMutations
{
    [UseApplicationDbContext]
    public async Task<CustomersPayload> AddCustomersAsync(CustomersInput input, [ScopedService] ApplicationDbContext context)
    {
        if (string.IsNullOrEmpty(input.Name))
        {
            return new CustomersPayload(new ApiError[]
            {
                new (message: "Title cannot be empty", "TITLE_EMPTY")
            });
        }

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