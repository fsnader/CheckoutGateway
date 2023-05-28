using System.Data;

namespace CheckoutGateway.Infrastructure.Database;

public interface IDbConnectionFactory
{
    public IDbConnection GenerateConnection();
}