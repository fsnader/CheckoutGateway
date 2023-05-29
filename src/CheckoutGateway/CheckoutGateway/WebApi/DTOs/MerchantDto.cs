using CheckoutGateway.Domain;

namespace CheckoutGateway.WebApi.DTOs;

public class MerchantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ClientId { get; set; }
    public string SecretId { get; set; }

    public static MerchantDto CreateFromMerchant(Merchant merchant) => new()
    {
        Id = merchant.Id,
        Name = merchant.Name,
        ClientId = merchant.ClientId,
        SecretId = merchant.SecretId
    };

}