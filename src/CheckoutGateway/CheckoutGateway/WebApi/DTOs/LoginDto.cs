using System.ComponentModel.DataAnnotations;

namespace CheckoutGateway.WebApi.DTOs;

public class LoginDto
{
    [Required]
    public string ClientId { get; set; }
    
    [Required]
    public string SecretId { get; set; }
}