using CheckoutGateway.Domain;

namespace CheckoutGateway.WebApi.Authentication;

public interface IAuthenticationService
{
    public Guid? GetCurrentMerchantId();
    public Task<string?> CreateTokenAsync(string clientId, string secretId, CancellationToken cancellationToken);
}