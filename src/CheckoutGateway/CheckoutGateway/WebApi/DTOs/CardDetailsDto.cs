using System.ComponentModel.DataAnnotations;
using CheckoutGateway.Domain;

namespace CheckoutGateway.Api.DTOs;

public class CardDetailsDto
{
    /// <summary>
    /// The cardholder's name
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// Payment's card last four digits
    /// </summary>
    [Required]
    [MinLength(16)]
    [MaxLength(16)]
    public string Number { get; set; }

    /// <summary>
    /// Card scheme: Visa, Mastercard, Amex, etc
    /// </summary>
    [Required]
    public string Scheme { get; set; }

    /// <summary>
    /// Card expiration month
    /// </summary>
    [Required]
    [Range(1, 12)]
    public int ExpirationMonth { get; set; }

    /// <summary>
    /// Card expiration year
    /// </summary>
    [Required]
    [Range(1000, 9999)]
    public int ExpirationYear { get; set; }

    /// <summary>
    /// Card CVV number
    /// </summary>
    [Required]
    [Range(0, 999)]
    public int Cvv { get; set; }

    public CreditCard ConvertToCreditCard() =>
        new()
        {
            Name = Name,
            Number = Number,
            Scheme = Scheme,
            ExpirationMonth = ExpirationMonth,
            ExpirationYear = ExpirationYear,
            Cvv = Cvv
        };
}