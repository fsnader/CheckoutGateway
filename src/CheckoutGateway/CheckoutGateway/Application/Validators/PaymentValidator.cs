using CheckoutGateway.Domain;
using FluentValidation;

namespace CheckoutGateway.Application.Validators;

public class PaymentValidator : AbstractValidator<Payment>
{
    public PaymentValidator()
    {
        RuleFor(p => p.Amount).GreaterThan(0)
            .PrecisionScale(int.MaxValue, 2, true)
            .WithMessage("Amount must be a valid number with 2 decimal.");

        RuleFor(p => p.Currency)
            .NotEmpty()
            .Matches(@"^([A-Z]){3}$")
            .WithMessage("The Currency must follow the ISO-4217 convention. Ex.: EUR, USD, BRL, GBP");
        
        RuleFor(p => p.Card).NotEmpty()
            .SetValidator(new CreditCardValidator());
    }
}