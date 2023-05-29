using CheckoutGateway.Domain;
using CheckoutGateway.Infrastructure.Database.Abstractions;
using CheckoutGateway.Infrastructure.Repositories.Abstractions;
using CheckoutGateway.Infrastructure.Repositories.Queries;
using Dapper;

namespace CheckoutGateway.Infrastructure.Repositories;

public class PaymentsRepository : IPaymentsRepository
{
    private readonly IQueryExecutor _queryExecutor;

    public PaymentsRepository(IQueryExecutor queryExecutor) => _queryExecutor = queryExecutor;

    public async Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken)
    {
        return await _queryExecutor.ExecuteQueryAsync(async connection =>
        {
            var result = await connection.QueryAsync<Payment, CreditCard, Payment>(PaymentQueries.CreatePayment,
                (p, c) =>
                {
                    p.Card = c;
                    return p;
                },
                new
                {
                    payment.MerchantId,
                    payment.Amount,
                    payment.Currency,
                    Status = payment.Status.ToString(),
                    CardName = payment.Card.Name,
                    CardNumber = payment.Card.Number,
                    CardScheme = payment.Card.Scheme,
                    CardExpirationMonth = payment.Card.ExpirationMonth,
                    CardExpirationYear = payment.Card.ExpirationYear,
                    CardCvv = payment.Card.Cvv
                }, splitOn: "card_name");

            return result.FirstOrDefault() ?? new Payment();
        }, cancellationToken);
    }

    public async Task UpdatePaymentAsync(Payment createdPayment, string reason, CancellationToken cancellationToken)
    {
    }

    public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return null;
    }

    public async Task<IEnumerable<Payment>> ListByMerchantIdAsync(Guid merchantId, CancellationToken cancellationToken)
    {
        return Array.Empty<Payment>();
    }
}