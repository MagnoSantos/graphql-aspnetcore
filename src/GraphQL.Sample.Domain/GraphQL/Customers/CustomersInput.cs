using HotChocolate.Types.Relay;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

public record CustomersInput(string? Name,
                             string? LastName,
                             string? Cpf);