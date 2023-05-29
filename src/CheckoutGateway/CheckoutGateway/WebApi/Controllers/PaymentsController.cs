using CheckoutGateway.Application.UseCases.Payments.Abstractions;
using CheckoutGateway.WebApi.Authentication;
using CheckoutGateway.WebApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutGateway.WebApi.Controllers;

[Route("api/payments"), Authorize]
[ApiController]
public class PaymentsController : BaseController
{
    private readonly IGetPaymentById _getPaymentById;
    private readonly IGetPaymentsList _getPaymentsList;
    private readonly ICreatePayment _createPayment;
    private readonly IAuthenticationService _authenticationService;

    public PaymentsController(
        IGetPaymentById getPaymentById,
        IGetPaymentsList getPaymentsList,
        ICreatePayment createPayment, IAuthenticationService authenticationService)
    {
        _getPaymentById = getPaymentById;
        _getPaymentsList = getPaymentsList;
        _createPayment = createPayment;
        _authenticationService = authenticationService;
    }
    
    /// <summary>
    /// Returns a Payment by UUID
    /// </summary>
    /// <param name="id">Payment UUID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var merchantId = _authenticationService.GetCurrentMerchantId();
        if (merchantId is null) return Unauthorized();
        
        var result = await _getPaymentById.ExecuteAsync(id, merchantId.Value, cancellationToken);
        return UseCaseActionResult(result, PaymentDto.CreateFromPayment);
    }
    
    /// <summary>
    /// Returns a list of merchant filtered payments
    /// </summary>
    /// <response code="200">List of merchant's requests</response>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PaymentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListPaymentsAsync(CancellationToken cancellationToken)
    {
        var merchantId = _authenticationService.GetCurrentMerchantId();
        if (merchantId is null) return Unauthorized();
        
        var result = await _getPaymentsList.ExecuteAsync(merchantId.Value, cancellationToken);

        return UseCaseActionResult(result, PaymentDto.CreateFromCollection);
    }


    /// <summary>
    /// Generates and processes a payment
    /// </summary>
    /// <param name="payment"></param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/payments=
    ///     {
    ///         "amount": 550,
    ///         "currency": "USD",
    ///         "card": {
    ///             "name": "Felipe Nader",
    ///             "number": "4141414141414141",
    ///             "scheme": "VISA",
    ///             "expirationMonth": 11,
    ///             "expirationYear": 2025,
    ///             "cvv": 999
    ///         }
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Processed payment response</response>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreatePaymentAsync([FromBody]CreatePaymentDto payment, CancellationToken cancellationToken)
    {
        var merchantId = _authenticationService.GetCurrentMerchantId();
        if (merchantId is null) return Unauthorized();
        
        var result = await _createPayment.ExecuteAsync(
            payment.ConvertToPayment(merchantId.Value), 
            cancellationToken);
        
        return UseCaseActionResult(result, PaymentDto.CreateFromPayment);
    }
}