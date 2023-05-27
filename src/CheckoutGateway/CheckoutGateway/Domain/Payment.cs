namespace CheckoutGateway.Domain;

public class Payment
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public PaymentStatus Status { get; set; }
    public CreditCard Card { get; set; }
}