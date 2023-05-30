using CheckoutGateway.Domain;
using CheckoutGateway.Domain.Abstractions;

namespace CheckoutGateway.Infrastructure.Gateways;

/// <summary>
/// CKO bank simulator
/// </summary>
public class BankGateway : IBankGateway
{
    private readonly Random _random;
    public BankGateway()
    {
        _random = new Random();
    }
    public async Task<PaymentGatewayResponse> ProcessPaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        await Task.Delay(200, cancellationToken);

        return _random.Next(1, 6) switch
        {
            1 => new PaymentGatewayResponse
            {
                Id = Guid.NewGuid(),
                Success = false,
                Error = "Connection error when trying to reach the CKO Bank. Please try again later"
            },
            2 => new PaymentGatewayResponse
            {
                Id = Guid.NewGuid(),
                Success = false,
                Error = "Transaction denied. Customer doesn't have funds."
            },
            _ => new PaymentGatewayResponse
            {
                Id = Guid.NewGuid(),
                Success = true
            }
        };
    }
}