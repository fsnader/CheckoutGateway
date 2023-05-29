using CheckoutGateway.Domain;

namespace CheckoutGateway.Infrastructure.Repositories.Abstractions;

public interface IMerchantsRepository
{
    public Task<Merchant> CreateAsync(Merchant merchant, CancellationToken cancellationToken);
    public Task<Merchant> GetByClientId(string clientId, CancellationToken cancellationToken);
}