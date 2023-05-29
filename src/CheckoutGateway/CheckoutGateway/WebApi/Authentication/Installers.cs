namespace CheckoutGateway.WebApi.Authentication;

public static class Installers
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection serviceCollection) =>
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
}