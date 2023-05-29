using CheckoutGateway.Infrastructure.Database.Abstractions;

namespace CheckoutGateway.Infrastructure.Database;

public static class Installer
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection) =>
        serviceCollection
            .AddSingleton<IDbConnectionFactory, DbConnectionFactory>()
            .AddSingleton<IQueryExecutor, QueryExecutor>();
}