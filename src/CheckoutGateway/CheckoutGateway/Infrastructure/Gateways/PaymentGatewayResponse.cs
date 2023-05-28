namespace CheckoutGateway.Infrastructure.Gateways;

public class PaymentGatewayResponse
{
    public bool Success { get; set; }
    public string? Error { get; set; }
}