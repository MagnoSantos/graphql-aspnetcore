using GraphQL.Sample.Data.Entities;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

public class AddCustomersPayload
{
    public AddCustomersPayload(Customer customer)
    {
        Customer = customer;
    }

    public Customer Customer { get; set; }
}