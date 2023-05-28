using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Domain;

namespace CheckoutGateway.Application.UseCases.Payments.Abstractions;

public interface IGetPaymentById
{
    public Task<UseCaseResult<Payment>> ExecuteAsync(Guid id, Guid merchantId, CancellationToken cancellationToken);
}