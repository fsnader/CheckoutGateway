using CheckoutGateway.Domain;

namespace CheckoutGateway.Api.DTOs;

public class PaymentDto
{
    /// <summary>
    /// Payment UUID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Payment ID on the gateway
    /// </summary>
    public Guid BankExternalId { get; set; }
    
    /// <summary>
    /// Payment amount
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Payment ISO 4217 Currency code
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Payment's card last four digits
    /// </summary>
    public string CardLastFourDigits { get; set; }
    
    /// <summary>
    /// Card scheme: Visa, Mastercard, Amex, etc
    /// </summary>
    public string CardScheme { get; set; }
}