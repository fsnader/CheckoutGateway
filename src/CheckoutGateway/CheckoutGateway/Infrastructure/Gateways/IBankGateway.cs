using CheckoutGateway.Domain;

namespace CheckoutGateway.Infrastructure.Gateways;

public interface IBankGateway
{
    Task<PaymentGatewayResponse> ProcessPaymentAsync(Payment payment, CancellationToken cancellationToken);
}