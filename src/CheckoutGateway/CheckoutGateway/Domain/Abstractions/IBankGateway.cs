using CheckoutGateway.Infrastructure.Gateways;

namespace CheckoutGateway.Domain.Abstractions;

public interface IBankGateway
{
    Task<PaymentGatewayResponse> ProcessPaymentAsync(Payment payment, CancellationToken cancellationToken);
}