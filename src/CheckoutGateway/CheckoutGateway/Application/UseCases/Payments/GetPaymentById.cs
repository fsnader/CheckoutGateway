using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Application.UseCases.Payments.Abstractions;
using CheckoutGateway.Domain;
using CheckoutGateway.Domain.Abstractions;

namespace CheckoutGateway.Application.UseCases.Payments;

public class GetPaymentById : IGetPaymentById
{
    private readonly IPaymentsRepository _paymentsRepository;

    public GetPaymentById(IPaymentsRepository paymentsRepository) =>
        _paymentsRepository = paymentsRepository;

    public async Task<UseCaseResult<Payment>> ExecuteAsync(Guid id, Guid merchantId, CancellationToken cancellationToken)
    {
        var payment = await _paymentsRepository.GetByIdAsync(id, cancellationToken);

        if (payment is null || payment.MerchantId != merchantId)
        {
            return UseCaseResult<Payment>.NotFound("Payment not found");
        }
        
        return UseCaseResult<Payment>.Success(payment);
    }
}