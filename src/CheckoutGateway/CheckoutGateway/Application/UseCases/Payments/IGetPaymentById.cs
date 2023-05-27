using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Domain;

namespace CheckoutGateway.Application.UseCases.Payments;

public interface IGetPaymentById
{
    public Task<UseCaseResult<Payment>> ExecuteAsync(Guid id, CancellationToken cancellationToken);
}