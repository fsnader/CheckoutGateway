using System.ComponentModel.DataAnnotations;
using CheckoutGateway.Domain;

namespace CheckoutGateway.Api.DTOs;

public class CreatePaymentDto
{
    /// <summary>
    /// Payment amount
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue)]
    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Payment ISO 4217 Currency code
    /// </summary>
    [Required]
    [MinLength(3)]
    [MaxLength(3)]
    public string Currency { get; set; }
    
    /// <summary>
    /// Payment Status: "Authorized", "Pending", "InProgress", "Declined" or "Processed"
    /// </summary>
    [Required]
    public PaymentStatus Status { get; set; }
    
    /// <summary>
    /// Credit Card information
    /// </summary>
    [Required]
    public CardDetailsDto Card { get; set; }
}