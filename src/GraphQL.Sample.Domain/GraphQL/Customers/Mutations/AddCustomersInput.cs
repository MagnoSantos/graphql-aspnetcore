namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

public record AddCustomersInput(Guid Id,
                                string? Name,
                                string? LastName,
                                string? Cpf);