using CheckoutGateway.Domain;

namespace CheckoutGateway.Infrastructure.Repositories.Abstractions;

public interface IPaymentsRepository
{
    Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken);
    Task UpdatePaymentAsync(Payment createdPayment, string reason, CancellationToken cancellationToken);
    Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> ListByMerchantIdAsync(Guid merchantId, CancellationToken cancellationToken);
}