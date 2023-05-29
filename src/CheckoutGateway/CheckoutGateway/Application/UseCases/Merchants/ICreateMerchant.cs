using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Domain;

namespace CheckoutGateway.Application.UseCases.Merchants;

public interface ICreateMerchant
{
    public Task<UseCaseResult<Merchant>> ExecuteAsync(string name, string clientId, CancellationToken cancellationToken);
}