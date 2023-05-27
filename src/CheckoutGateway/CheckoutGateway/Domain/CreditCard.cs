namespace CheckoutGateway.Domain;

public class CreditCard
{
    public string Name { get; set; }
    public string Number { get; set; }
    public string Scheme { get; set; }
    public int ExpirationMonth { get; set; }
    public int ExpirationYear { get; set; }
    public int Cvv { get; set; }
}