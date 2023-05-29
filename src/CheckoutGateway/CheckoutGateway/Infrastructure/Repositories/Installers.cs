using CheckoutGateway.Domain.Abstractions;

namespace CheckoutGateway.Infrastructure.Repositories;

public static class Installers
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection) =>
        serviceCollection
            .AddScoped<IMerchantsRepository, MerchantsRepository>()
            .AddScoped<IPaymentsRepository, PaymentsRepository>();
}