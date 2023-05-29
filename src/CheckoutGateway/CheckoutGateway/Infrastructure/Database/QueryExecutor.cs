using System.Data;
using CheckoutGateway.Infrastructure.Database.Abstractions;
using Npgsql;

namespace CheckoutGateway.Infrastructure.Database;

public class QueryExecutor : IQueryExecutor
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly ILogger<IDbConnectionFactory> _logger;

    public QueryExecutor(IDbConnectionFactory dbConnectionFactory, ILogger<IDbConnectionFactory> logger)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _logger = logger;
    }

    public async Task<T> ExecuteQueryAsync<T>(Func<IDbConnection, Task<T>> queryDelegate, CancellationToken cancellationToken)
    {
        try
        {
            using var connection = _dbConnectionFactory.GenerateConnection();

            return await queryDelegate(connection);
        }
        catch (PostgresException ex)
        {
            _logger.LogError(ex, "Postgresql error executing query");

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Postgresql error executing query");
            throw;
        }
    }
}