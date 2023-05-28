using CheckoutGateway.Domain;

namespace CheckoutGateway.Infrastructure.Repositories.Abstractions;

public interface ICreditCardRepository
{
    Task CreateAsync(Guid paymentId, CreditCard paymentCard, CancellationToken cancellationToken);
}