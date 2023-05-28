using CheckoutGateway.Domain;
using CheckoutGateway.Infrastructure.Repositories.Abstractions;

namespace CheckoutGateway.Infrastructure.Repositories;

public class CreditCardRepository : ICreditCardRepository
{
    public Task CreateAsync(Guid paymentId, CreditCard paymentCard, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}