using CheckoutGateway.Domain;
using CheckoutGateway.Infrastructure.Gateways.Abstractions;

namespace CheckoutGateway.Infrastructure.Gateways;

public class BankGateway : IBankGateway
{
    public async Task<PaymentGatewayResponse> ProcessPaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        return new PaymentGatewayResponse
        {
            Id = Guid.NewGuid(),
            Success = true
        };
    }
}