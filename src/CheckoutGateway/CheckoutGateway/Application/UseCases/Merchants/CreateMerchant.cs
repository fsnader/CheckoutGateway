using CheckoutGateway.Application.UseCases.Merchants.Abstractions;
using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Domain;
using CheckoutGateway.Domain.Abstractions;

namespace CheckoutGateway.Application.UseCases.Merchants;

public class CreateMerchant : ICreateMerchant
{
    private readonly IMerchantsRepository _merchantsRepository;

    public CreateMerchant(IMerchantsRepository merchantsRepository) => _merchantsRepository = merchantsRepository;

    public async Task<UseCaseResult<Merchant>> ExecuteAsync(string name, string clientId, CancellationToken cancellationToken)
    {
        var existing = await _merchantsRepository.GetByClientId(clientId, cancellationToken);

        if (existing is not null)
        {
            return UseCaseResult<Merchant>.BadRequest("Invalid clientId");
        }
        
        var merchant = new Merchant
        {
            Name = name,
            ClientId = clientId,
            SecretId = Guid.NewGuid().ToString()
        };
        
        var created = await _merchantsRepository.CreateAsync(merchant, cancellationToken);

        return UseCaseResult<Merchant>.Success(created);
    }
}