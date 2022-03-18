using GraphQL.Sample.Data.Entities;
using GraphQL.Sample.Infra.Data.Repositories;
using HotChocolate.Types;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

[ExtendObjectType("Mutation")]
public class CustomersMutations
{
    private readonly ICustomerRepository _customerRepository;

    public CustomersMutations(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<CustomersPayload> AddCustomersAsync(CustomersInput input)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Cpf = input.Cpf,
            LastName = input.LastName
        };

        await _customerRepository.Add(customer);

        return new CustomersPayload(customer);
    }
}