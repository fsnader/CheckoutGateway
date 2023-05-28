using CheckoutGateway.Domain;

namespace CheckoutGateway.Infrastructure.Gateways.Abstractions;

public interface IBankGateway
{
    Task<PaymentGatewayResponse> ProcessPaymentAsync(Payment payment, CancellationToken cancellationToken);
}