using System.Data;
using CheckoutGateway.Infrastructure.Database.Abstractions;
using Npgsql;

namespace CheckoutGateway.Infrastructure.Database;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string? _connectionString;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Postgresql");
    }
    
    public IDbConnection GenerateConnection()
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new NpgsqlException("Please provide a valid connection string");
        }
        
        var connection = new NpgsqlConnection(_connectionString);
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        return connection;
    }
}