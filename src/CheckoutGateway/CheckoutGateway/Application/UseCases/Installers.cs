using CheckoutGateway.Application.UseCases.Payments;
using CheckoutGateway.Application.UseCases.Payments.Abstractions;

namespace CheckoutGateway.Application.UseCases;

public static class Installers
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection) => serviceCollection
        .AddScoped<ICreatePayment, CreatePayment>()
        .AddScoped<IGetPaymentById, GetPaymentById>()
        .AddScoped<IGetPaymentsList, GetPaymentsList>();
}