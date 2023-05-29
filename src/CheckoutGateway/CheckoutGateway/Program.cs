using System.Reflection;
using System.Text.Json.Serialization;
using CheckoutGateway.Api.Middlewares;
using CheckoutGateway.Application.UseCases;
using CheckoutGateway.Infrastructure.Database;
using CheckoutGateway.Infrastructure.Gateways;
using CheckoutGateway.Infrastructure.Repositories;
using CheckoutGateway.WebApi.Authentication;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddScoped<GlobalExceptionsMiddleware>()
    .AddAuthenticationServices(builder.Configuration)
    .AddGateways()
    .AddDatabase()
    .AddRepositories()
    .AddUseCases();

builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});

builder.Services
    .AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services
    .AddEndpointsApiExplorer()
    .AddHttpContextAccessor();

builder.Services
    .AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

var app = builder.Build();

app
    .UseMiddleware<GlobalExceptionsMiddleware>()
    .UseHttpsRedirection()
    .UseAuthorization()
    .UseSwagger()
    .UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });


app.MapControllers();

app.Run();