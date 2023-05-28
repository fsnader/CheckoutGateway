using CheckoutGateway.Domain;
using CheckoutGateway.Infrastructure.Repositories.Abstractions;

namespace CheckoutGateway.Infrastructure.Repositories;

public class PaymentsRepository : IPaymentsRepository
{
    public Task<Payment> CreateAsync(Payment payment)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePaymentStatusAsync(Payment createdPayment, PaymentStatus status, string reason)
    {
        throw new NotImplementedException();
    }
}