using System.Data;

namespace CheckoutGateway.Infrastructure.Database;

public interface IQueryExecutor
{
    public Task<T> ExecuteQueryAsync<T>(Func<IDbConnection, Task<T>> queryDelegate, CancellationToken cancellationToken);
}