using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CheckoutGateway.WebApi.Authentication;

public static class Installers
{
    public static IServiceCollection AddAuthenticationServices(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddAuthentication().AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    configuration.GetSection("AppSettings:Token").Value!))
            };
        });
        
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();

        return serviceCollection;
    }
}