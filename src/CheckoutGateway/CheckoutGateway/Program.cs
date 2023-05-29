using System.Reflection;
using System.Text.Json.Serialization;
using CheckoutGateway.Api.Middlewares;
using CheckoutGateway.Application.UseCases;
using CheckoutGateway.Infrastructure.Database;
using CheckoutGateway.Infrastructure.Gateways;
using CheckoutGateway.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddScoped<GlobalExceptionsMiddleware>()
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