using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Domain;

namespace CheckoutGateway.Application.UseCases.Payments;

public interface ICreatePayment
{
    public Task<UseCaseResult<Payment>> ExecuteAsync(Payment payment, CancellationToken cancellationToken);
}