using CheckoutGateway.Application.UseCases.Merchants;
using CheckoutGateway.Application.UseCases.Merchants.Abstractions;
using CheckoutGateway.Application.UseCases.Payments;
using CheckoutGateway.Application.UseCases.Payments.Abstractions;
using CheckoutGateway.Application.Validators;
using CheckoutGateway.Domain;
using FluentValidation;

namespace CheckoutGateway.Application.UseCases;

public static class Installers
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection) => serviceCollection
        .AddValidators()
        .AddScoped<ICreateMerchant, CreateMerchant>()
        .AddScoped<ICreatePayment, CreatePayment>()
        .AddScoped<IGetPaymentById, GetPaymentById>()
        .AddScoped<IGetPaymentsList, GetPaymentsList>();

    private static IServiceCollection AddValidators(this IServiceCollection serviceCollection) => serviceCollection
        .AddScoped<IValidator<Payment>, PaymentValidator>();
}