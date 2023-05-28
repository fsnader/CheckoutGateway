using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Application.UseCases.Payments.Abstractions;
using CheckoutGateway.Domain;
using CheckoutGateway.Infrastructure.Repositories.Abstractions;

namespace CheckoutGateway.Application.UseCases.Payments;

public class GetPaymentById : IGetPaymentById
{
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly ICreditCardRepository _creditCardRepository;

    public GetPaymentById(
        IPaymentsRepository paymentsRepository, 
        ICreditCardRepository creditCardRepository)
    {
        _paymentsRepository = paymentsRepository;
        _creditCardRepository = creditCardRepository;
    }
    
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