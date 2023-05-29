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

    public async Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken) =>
        await _queryExecutor.ExecuteQueryAsync(async connection =>
        {
            var result = await connection.QueryAsync<Payment, CreditCard, Payment>(PaymentQueries.Create,
                MapPaymentAndCreditCard,
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
                }, splitOn: "name");

            return result.FirstOrDefault()!;
        }, cancellationToken);

    public async Task UpdatePaymentAsync(Payment createdPayment, string reason, CancellationToken cancellationToken)
    {
    }

    public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _queryExecutor.ExecuteQueryAsync(async connection =>
        {
            var result = await connection.QueryAsync<Payment, CreditCard, Payment>(
                PaymentQueries.GetById,
                MapPaymentAndCreditCard,
                new { Id = id }, splitOn: "name");

            return result.SingleOrDefault();
        }, cancellationToken);

    public async Task<IEnumerable<Payment>>
        ListByMerchantIdAsync(Guid merchantId, CancellationToken cancellationToken) =>
        await _queryExecutor.ExecuteQueryAsync(async connection =>
            await connection.QueryAsync<Payment, CreditCard, Payment>(
                PaymentQueries.ListByMerchantId,
                MapPaymentAndCreditCard,
                new { MerchantId = merchantId }, splitOn: "name"), cancellationToken);

    private static Payment MapPaymentAndCreditCard(Payment p, CreditCard c)
    {
        p.Card = c;
        return p;
    }
}