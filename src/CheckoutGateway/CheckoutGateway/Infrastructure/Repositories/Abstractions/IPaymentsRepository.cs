using CheckoutGateway.Domain;

namespace CheckoutGateway.Infrastructure.Repositories.Abstractions;

public interface IPaymentsRepository
{
    Task<Payment> CreateAsync(Payment payment);
    
    Task UpdatePaymentStatusAsync(Payment createdPayment, PaymentStatus status, string reason);
}