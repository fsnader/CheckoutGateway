using CheckoutGateway.Domain;
using CheckoutGateway.Infrastructure.Database.Abstractions;
using CheckoutGateway.Infrastructure.Repositories.Abstractions;
using CheckoutGateway.Infrastructure.Repositories.Queries;
using Dapper;

namespace CheckoutGateway.Infrastructure.Repositories;

public class MerchantsRepository : IMerchantsRepository
{
    private readonly IQueryExecutor _queryExecutor;

    public MerchantsRepository(IQueryExecutor queryExecutor) => _queryExecutor = queryExecutor;

    public Task<Merchant> CreateAsync(Merchant merchant, CancellationToken cancellationToken) =>
        _queryExecutor.ExecuteQueryAsync(connection => connection.QueryFirstOrDefaultAsync<Merchant>(
            MerchantQueries.Create,
            new
            {
                merchant.Name,
                merchant.ClientId,
                merchant.SecretId
            }), cancellationToken);

    public Task<Merchant> GetByClientId(string clientId, CancellationToken cancellationToken) =>
        _queryExecutor.ExecuteQueryAsync(connection =>
                connection.QueryFirstOrDefaultAsync<Merchant>(
                    MerchantQueries.GetById,
                    new { ClientId = clientId }),
            cancellationToken);
}