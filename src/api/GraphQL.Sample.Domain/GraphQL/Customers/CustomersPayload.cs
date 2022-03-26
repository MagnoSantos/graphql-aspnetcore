using GraphQL.Sample.Data.Entities;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

public class CustomersPayload 
{
    public CustomersPayload(Customer customer)
    {
        Customer = customer;
    }

    public Customer? Customer { get; set; }
}