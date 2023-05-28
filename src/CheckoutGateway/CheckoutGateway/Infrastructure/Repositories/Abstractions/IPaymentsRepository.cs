using CheckoutGateway.Domain;

namespace CheckoutGateway.Infrastructure.Repositories.Abstractions;

public interface IPaymentsRepository
{
    Task<Payment> CreateAsync(Payment payment);
    
    Task UpdatePaymentAsync(Payment createdPayment, string reason);
    Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> ListByMerchantIdAsync(Guid merchantId, CancellationToken cancellationToken);
}