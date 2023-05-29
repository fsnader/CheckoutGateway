using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Application.UseCases.Payments.Abstractions;
using CheckoutGateway.Domain;
using CheckoutGateway.Domain.Abstractions;
using FluentValidation;

namespace CheckoutGateway.Application.UseCases.Payments;

public class CreatePayment : ICreatePayment
{
    private readonly IBankGateway _bankGateway;
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IValidator<Payment> _validator;

    public CreatePayment(
        IBankGateway bankGateway,
        IPaymentsRepository paymentsRepository,
        IValidator<Payment> validator)
    {
        _bankGateway = bankGateway;
        _paymentsRepository = paymentsRepository;
        _validator = validator;
    }
    
    public async Task<UseCaseResult<Payment>> ExecuteAsync(Payment payment, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(payment, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return UseCaseResult<Payment>.BadRequest(validationResult.ToString(","));
        }
        
        var createdPayment = await _paymentsRepository.CreateAsync(payment, cancellationToken);
        
        var result = await _bankGateway.ProcessPaymentAsync(payment, cancellationToken);

        if (result.Success)
        {
            await createdPayment.UpdateToProcessed(result.Id, _paymentsRepository, cancellationToken);
            return UseCaseResult<Payment>.Success(createdPayment);
        }

        await createdPayment.UpdateToDeclined(result.Error!, _paymentsRepository, cancellationToken);
        return UseCaseResult<Payment>.Rejected(result.Error!);
    }

    private bool IsInputValid(Payment payment)
    {
        return true;
    }
}