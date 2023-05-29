using CheckoutGateway.Domain;
using CheckoutGateway.Domain.Abstractions;

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