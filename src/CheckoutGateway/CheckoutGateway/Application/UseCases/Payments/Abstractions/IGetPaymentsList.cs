using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Domain;

namespace CheckoutGateway.Application.UseCases.Payments.Abstractions;

public interface IGetPaymentsList
{
    public Task<UseCaseResult<IEnumerable<Payment>>> ExecuteAsync(CancellationToken cancellationToken);
}