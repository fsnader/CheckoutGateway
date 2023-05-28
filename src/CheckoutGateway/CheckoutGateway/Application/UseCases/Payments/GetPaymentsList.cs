using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Application.UseCases.Payments.Abstractions;
using CheckoutGateway.Domain;

namespace CheckoutGateway.Application.UseCases.Payments;

public class GetPaymentsList : IGetPaymentsList
{
    public Task<UseCaseResult<IEnumerable<Payment>>> ExecuteAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}