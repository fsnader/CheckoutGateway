using CheckoutGateway.Infrastructure.Gateways.Abstractions;

namespace CheckoutGateway.Infrastructure.Gateways;

public static class Installers
{
    public static IServiceCollection AddGateways(this IServiceCollection serviceCollection) =>
        serviceCollection.AddScoped<IBankGateway, BankGateway>();
}