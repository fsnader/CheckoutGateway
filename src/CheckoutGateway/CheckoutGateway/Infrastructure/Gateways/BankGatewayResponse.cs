namespace CheckoutGateway.Infrastructure.Gateways;

public class PaymentGatewayResponse
{
    public Guid Id { get; set; }
    public bool Success { get; set; }
    public string? Error { get; set; }
}