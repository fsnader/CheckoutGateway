using CheckoutGateway.Domain;

namespace CheckoutGateway.Infrastructure.Gateways;

public class BankGateway : IBankGateway
{
    public Task<PaymentGatewayResponse> ProcessPaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}