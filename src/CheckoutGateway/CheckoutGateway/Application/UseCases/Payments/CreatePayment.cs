using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Application.UseCases.Payments.Abstractions;
using CheckoutGateway.Domain;
using CheckoutGateway.Infrastructure.Gateways.Abstractions;
using CheckoutGateway.Infrastructure.Repositories.Abstractions;

namespace CheckoutGateway.Application.UseCases.Payments;

public class CreatePayment : ICreatePayment
{
    private readonly IBankGateway _bankGateway;
    private readonly IPaymentsRepository _paymentsRepository;

    public CreatePayment(
        IBankGateway bankGateway,
        IPaymentsRepository paymentsRepository)
    {
        _bankGateway = bankGateway;
        _paymentsRepository = paymentsRepository;
    }
    
    public async Task<UseCaseResult<Payment>> ExecuteAsync(Payment payment, CancellationToken cancellationToken)
    {
        if (!IsInputValid(payment))
        {
            // TODO: Use FluentValidator
            return UseCaseResult<Payment>.BadRequest("Invalid parameters");
        }
        
        // TODO: Idempotency validation
        
        var createdPayment = await _paymentsRepository.CreateAsync(payment);
        // await _creditCardRepository.CreateAsync(createdPayment.Id, payment.Card, cancellationToken);
        
        var result = await _bankGateway.ProcessPaymentAsync(payment, cancellationToken);

        if (result.Success)
        {
            await createdPayment.UpdateToProcessed(result.Id, _paymentsRepository);
            return UseCaseResult<Payment>.Success(createdPayment);
        }

        await createdPayment.UpdateToDeclined(result.Error!, _paymentsRepository);
        return UseCaseResult<Payment>.Rejected(result.Error!);
    }

    private bool IsInputValid(Payment payment)
    {
        return true;
    }
}