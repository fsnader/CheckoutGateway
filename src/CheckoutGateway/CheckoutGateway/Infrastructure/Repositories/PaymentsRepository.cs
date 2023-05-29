using CheckoutGateway.Domain;
using CheckoutGateway.Infrastructure.Repositories.Abstractions;

namespace CheckoutGateway.Infrastructure.Repositories;

public class PaymentsRepository : IPaymentsRepository
{
    public async Task<Payment> CreateAsync(Payment payment)
    {
        return payment;
    }

    public async Task UpdatePaymentAsync(Payment createdPayment, string reason)
    {
        
    }

    public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return null;
    }

    public async Task<IEnumerable<Payment>> ListByMerchantIdAsync(Guid merchantId, CancellationToken cancellationToken)
    {
        return Array.Empty<Payment>();
    }
}