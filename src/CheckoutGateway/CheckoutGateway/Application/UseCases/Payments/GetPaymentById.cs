using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Application.UseCases.Payments.Abstractions;
using CheckoutGateway.Domain;

namespace CheckoutGateway.Application.UseCases.Payments;

public class GetPaymentById : IGetPaymentById
{
    public Task<UseCaseResult<Payment>> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}