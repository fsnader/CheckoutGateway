using CheckoutGateway.Domain;

namespace CheckoutGateway.WebApi.DTOs;

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
    /// Payment Status
    /// </summary>
    public PaymentStatus Status { get; set; }

    /// <summary>
    /// Payment's card last four digits
    /// </summary>
    public string CardLastFourDigits { get; set; }
    
    /// <summary>
    /// Card scheme: Visa, Mastercard, Amex, etc
    /// </summary>
    public string CardScheme { get; set; }
    
    /// <summary>
    /// Payment Created datetime
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    /// Payment Updated datetime
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    public static PaymentDto CreateFromPayment(Payment payment) =>
        new()
        {
            Id = payment.Id,
            BankExternalId = payment.BankExternalId,
            Amount = payment.Amount,
            Currency = payment.Currency,
            Status = payment.Status,
            CardLastFourDigits = payment.Card.LastFourDigits,
            CardScheme = payment.Card.Scheme,
            CreatedAt = payment.CreatedAt,
            UpdatedAt = payment.UpdatedAt
        };

    public static IEnumerable<PaymentDto> CreateFromCollection(IEnumerable<Payment> payments) =>
        payments.Select(CreateFromPayment);
}