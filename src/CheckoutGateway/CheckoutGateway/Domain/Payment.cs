namespace CheckoutGateway.Domain;

public class Payment
{
    public Guid Id { get; set; }
    public Guid BankExternalId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public PaymentStatus Status { get; set; }
    public CreditCard Card { get; set; }
}