using GraphQL.Sample.Data.Entities;
using GraphQL.Sample.Infra.Data.Repositories;
using GraphQL.Sample.Infra.Data.UoW;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

[ExtendObjectType("Mutation")]
public class CustomersMutations
{
    public async Task<CustomersPayload> AddCustomersAsync(CustomersInput input, 
                                                         [Service] ICustomerRepository customerRepository, 
                                                         [Service] IUnitOfWork unitOfWork)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Cpf = input.Cpf,
            LastName = input.LastName
        };

        var addCustomerTask = customerRepository.Add(customer);
        var unitOfWorkTask = unitOfWork.CommitAsync();

        await Task.WhenAll(addCustomerTask, unitOfWorkTask);

        return new CustomersPayload(customer);
    }
}