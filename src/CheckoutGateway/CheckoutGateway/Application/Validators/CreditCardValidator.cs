using CheckoutGateway.Domain;
using FluentValidation;

namespace CheckoutGateway.Application.Validators;

public class CreditCardValidator : AbstractValidator<CreditCard>
{
    public CreditCardValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(c => c.Number)
            .CreditCard();
        
        RuleFor(c => c.Scheme)
            .NotEmpty()
            .Length(3);
        
        RuleFor(c => c.ExpirationMonth)
            .InclusiveBetween(1, 12);
        
        RuleFor(c => c.ExpirationYear)
            .GreaterThan(DateTime.Now.Year);
        
        RuleFor(c => c.Cvv).NotEmpty()
            .InclusiveBetween(1, 999);
    }
}