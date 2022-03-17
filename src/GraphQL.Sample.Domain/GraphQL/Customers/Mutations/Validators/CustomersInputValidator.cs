using FluentValidation;

namespace GraphQL.Sample.Domain.GraphQL.Customers.Mutations.Validators;

public class CustomersInputValidator : AbstractValidator<CustomersInput>
{
    public CustomersInputValidator()
    {
        RuleFor(input => input.Cpf)
            .NotEmpty()
            .NotNull()
            .WithMessage("Property cannot be null or empty");

        RuleFor(input => input.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Property cannot be null or empty");
    }
}