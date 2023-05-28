namespace CheckoutGateway.Domain;

public class Merchant
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ClientId { get; set; }
    public string SecretId { get; set; }
}