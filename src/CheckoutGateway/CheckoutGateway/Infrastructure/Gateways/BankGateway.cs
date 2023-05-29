using CheckoutGateway.Domain;
using CheckoutGateway.Domain.Abstractions;

namespace CheckoutGateway.Infrastructure.Gateways;

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

        return _random.Next(1, 3) switch
        {
            1 => new PaymentGatewayResponse
            {
                Id = Guid.NewGuid(),
                Success = true
            },
            2 => new PaymentGatewayResponse
            {
                Id = Guid.NewGuid(),
                Success = false,
                Error = "Connection error"
            },
            3 => new PaymentGatewayResponse
            {
                Id = Guid.NewGuid(),
                Success = false,
                Error = "Transaction denied. Customer doesn't have funds."
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}