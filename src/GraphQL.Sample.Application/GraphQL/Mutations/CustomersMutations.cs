using GraphQL.Sample.Data.Entities;
using GraphQL.Sample.Infra.Data.Repositories;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

[ExtendObjectType("Mutation")]
public class CustomersMutations
{
    public async Task<CustomersPayload> AddCustomersAsync(CustomersInput input, [Service] ICustomerRepository customerRepository)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Cpf = input.Cpf,
            LastName = input.LastName
        };

        await customerRepository.Add(customer);

        return new CustomersPayload(customer);
    }
}