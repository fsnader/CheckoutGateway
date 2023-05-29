using System.ComponentModel.DataAnnotations;

namespace CheckoutGateway.WebApi.DTOs;

public class CreateMerchantDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string ClientId { get; set; }
}