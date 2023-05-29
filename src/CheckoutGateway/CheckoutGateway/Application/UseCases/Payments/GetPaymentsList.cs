using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Application.UseCases.Payments.Abstractions;
using CheckoutGateway.Domain;
using CheckoutGateway.Domain.Abstractions;

namespace CheckoutGateway.Application.UseCases.Payments;

public class GetPaymentsList : IGetPaymentsList
{
    private readonly IPaymentsRepository _paymentsRepository;

    public GetPaymentsList(IPaymentsRepository paymentsRepository)
    {
        _paymentsRepository = paymentsRepository;
    }
    
    public async Task<UseCaseResult<IEnumerable<Payment>>> ExecuteAsync(Guid merchantId, CancellationToken cancellationToken)
    {
        var payments = await _paymentsRepository.ListByMerchantIdAsync(merchantId, cancellationToken);
        
        return UseCaseResult<IEnumerable<Payment>>.Success(payments);
    }
}