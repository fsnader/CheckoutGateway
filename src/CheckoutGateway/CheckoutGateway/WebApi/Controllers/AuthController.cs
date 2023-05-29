using CheckoutGateway.Application.UseCases.Merchants;
using CheckoutGateway.WebApi.Authentication;
using CheckoutGateway.WebApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutGateway.WebApi.Controllers;

[Route("api/auth")]
[ApiController, AllowAnonymous]
public class AuthController : BaseController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ICreateMerchant _createMerchant;

    public AuthController(IAuthenticationService authenticationService, ICreateMerchant createMerchant)
    {
        _authenticationService = authenticationService;
        _createMerchant = createMerchant;
    }
    
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] CreateMerchantDto merchantDto, CancellationToken cancellationToken)
    {
        var result = await _createMerchant.ExecuteAsync(
            merchantDto.Name,
            merchantDto.ClientId,
            cancellationToken);

        return UseCaseActionResult(result, MerchantDto.CreateFromMerchant);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> GetToken([FromBody] LoginDto login, CancellationToken cancellationToken)
    {
        var token = await _authenticationService.CreateTokenAsync(login.ClientId, login.SecretId, cancellationToken);

        if (token is null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }
}