using CheckoutGateway.Domain;
using CheckoutGateway.Infrastructure.Repositories.Abstractions;

namespace CheckoutGateway.Infrastructure.Repositories;

public class PaymentsRepository : IPaymentsRepository
{
    public Task<Payment> CreateAsync(Payment payment)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePaymentAsync(Payment createdPayment, string reason)
    {
        throw new NotImplementedException();
    }

    public Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Payment>> ListByMerchantIdAsync(Guid merchantId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}