using CheckoutGateway.Domain;
using CheckoutGateway.Domain.Abstractions;
using CheckoutGateway.Infrastructure.Database.Abstractions;
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
            payment.UpdatedAt = payment.CreatedAt = DateTimeOffset.UtcNow;
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
                    CardCvv = payment.Card.Cvv,
                    payment.CreatedAt,
                    payment.UpdatedAt
                }, splitOn: "name");

            return result.FirstOrDefault()!;
        }, cancellationToken);

    public async Task UpdatePaymentAsync(Payment payment, string reason, CancellationToken cancellationToken) =>
        await _queryExecutor.ExecuteQueryAsync(async connection => await connection.ExecuteAsync(PaymentQueries.UpdateById,
            new
            {
                payment.Id,
                payment.BankExternalId,
                payment.MerchantId,
                payment.Amount,
                payment.Currency,
                Status = payment.Status.ToString(),
                CardName = payment.Card.Name,
                CardNumber = payment.Card.Number,
                CardScheme = payment.Card.Scheme,
                CardExpirationMonth = payment.Card.ExpirationMonth,
                CardExpirationYear = payment.Card.ExpirationYear,
                CardCvv = payment.Card.Cvv,
                UpdatedAt = DateTimeOffset.UtcNow
            }), cancellationToken);

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