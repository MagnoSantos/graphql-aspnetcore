using GraphQL.Sample.Data.Entities;
using GraphQL.Sample.Domain.Abstractions.Common;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

public class CustomersPayload : Payload
{
    public CustomersPayload(Customer customer)
    {
        Customer = customer;
    }

    public CustomersPayload(IReadOnlyList<ApiError> errors) : base(errors)
    {
    }

    public Customer? Customer { get; set; }
}