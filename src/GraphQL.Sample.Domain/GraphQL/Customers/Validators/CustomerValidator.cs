using FluentValidation;
using GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Validators;

public class CustomerValidator : AbstractValidator<CustomersInput>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.LastName).NotEmpty().NotNull();
        RuleFor(customer => customer.Name).NotEmpty().NotNull();
        RuleFor(customer => customer.Cpf).NotEmpty().NotNull();
    }
}