using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators;

internal class CheckoutOrderCommandValidator : AbstractValidator<CheckOutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(o => o.UserName)
            .NotNull()
            .NotEmpty().WithMessage("{UserName} is requiered")
            .MaximumLength(70).WithMessage("{UserName} must not exced 70 characters");
        
        RuleFor(o => o.TotalPrice)
            .NotEmpty().WithMessage("{TotalPrice} is requiered")
            .GreaterThan(-1).WithMessage("{TotalPrice} should not be negative");

        RuleFor(o => o.EmailAddress)
            .NotEmpty().WithMessage("{EmailAddress} is requiered");

        RuleFor(o => o.FirstName)
          .NotEmpty()
          .NotNull()
          .WithMessage("{FirstName} is required");

        RuleFor(o => o.LastName)
            .NotEmpty()
            .NotNull()
            .WithMessage("{LastName} is required");

    }
}

