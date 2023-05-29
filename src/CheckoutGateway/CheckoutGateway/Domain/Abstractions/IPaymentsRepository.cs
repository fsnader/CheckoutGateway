namespace CheckoutGateway.Domain.Abstractions;

public interface IPaymentsRepository
{
    Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken);
    Task UpdatePaymentAsync(Payment payment, string reason, CancellationToken cancellationToken);
    Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> ListByMerchantIdAsync(Guid merchantId, CancellationToken cancellationToken);
}