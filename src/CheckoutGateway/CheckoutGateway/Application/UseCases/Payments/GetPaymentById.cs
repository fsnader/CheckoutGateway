using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Application.UseCases.Payments.Abstractions;
using CheckoutGateway.Domain;
using CheckoutGateway.Infrastructure.Repositories.Abstractions;

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
        
        // TODO: What about credit card ?

        return UseCaseResult<Payment>.Success(payment);
    }
}