using CheckoutGateway.Infrastructure.Repositories.Abstractions;

namespace CheckoutGateway.Infrastructure.Repositories;

public static class Installers
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection) =>
        serviceCollection
            .AddScoped<ICreditCardRepository, CreditCardRepository>()
            .AddScoped<IPaymentsRepository, PaymentsRepository>();
}