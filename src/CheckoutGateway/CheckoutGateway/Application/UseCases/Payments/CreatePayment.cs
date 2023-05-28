using CheckoutGateway.Application.UseCases.OutputPorts;
using CheckoutGateway.Application.UseCases.Payments.Abstractions;
using CheckoutGateway.Domain;

namespace CheckoutGateway.Application.UseCases.Payments;

public class CreatePayment : ICreatePayment
{
    public Task<UseCaseResult<Payment>> ExecuteAsync(Payment payment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}