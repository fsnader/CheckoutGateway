using System.Data;

namespace CheckoutGateway.Infrastructure.Database.Abstractions;

public interface IDbConnectionFactory
{
    public IDbConnection GenerateConnection();
}