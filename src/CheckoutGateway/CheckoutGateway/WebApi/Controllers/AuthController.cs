using CheckoutGateway.Application.UseCases.Merchants;
using CheckoutGateway.Application.UseCases.Merchants.Abstractions;
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
    
    /// <summary>
    /// Creates a merchant
    /// </summary>
    /// <param name="merchantDto"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Created merchant</response>
    /// <returns></returns>
    [HttpPost("signup")]
    [ProducesResponseType(typeof(MerchantDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Signup([FromBody] CreateMerchantDto merchantDto, CancellationToken cancellationToken)
    {
        var result = await _createMerchant.ExecuteAsync(
            merchantDto.Name,
            merchantDto.ClientId,
            cancellationToken);

        return UseCaseActionResult(result, MerchantDto.CreateFromMerchant);
    }
    
    /// <summary>
    /// Logins a merchant using its client_id and client_secret
    /// </summary>
    /// <param name="login"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Generated token</response>
    /// <returns></returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
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