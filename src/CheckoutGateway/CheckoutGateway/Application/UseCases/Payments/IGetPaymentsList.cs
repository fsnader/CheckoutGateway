using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Domain;

namespace CheckoutGateway.Application.UseCases.Payments;

public interface IGetPaymentList
{
    public Task<UseCaseResult<IEnumerable<Payment>>> ExecuteAsync(CancellationToken cancellationToken);
}