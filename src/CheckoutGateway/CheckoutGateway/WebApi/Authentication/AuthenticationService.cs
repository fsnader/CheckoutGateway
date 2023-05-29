using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CheckoutGateway.Domain.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace CheckoutGateway.WebApi.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMerchantsRepository _merchantsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public AuthenticationService(
        IMerchantsRepository merchantsRepository, 
        IHttpContextAccessor httpContextAccessor, 
        IConfiguration configuration)
    {
        _merchantsRepository = merchantsRepository;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public Guid? GetCurrentMerchantId()
    {
        var result = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (result is null)
        {
            return null;
        }
        
        return Guid.Parse(result);
    }

    public async Task<string?> CreateTokenAsync(string clientId, string secretId, CancellationToken cancellationToken)
    {
        var merchant = await _merchantsRepository.GetByClientId(clientId, cancellationToken);

        if (merchant is null || merchant.SecretId != secretId)
        {
            return null;
        }
        
        var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, merchant.Id.ToString()),
            new(ClaimTypes.Name, merchant.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!));

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: signingCredentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}