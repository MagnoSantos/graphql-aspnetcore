namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

public record CustomersInput(Guid Id,
                                string? Name,
                                string? LastName,
                                string? Cpf);