using CheckoutGateway.Application.UseCases.Payments.Abstractions;
using CheckoutGateway.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutGateway.WebApi.Controllers;

[Route("api/payments")]
[ApiController]
public class PaymentsController : BaseController
{
    private readonly IGetPaymentById _getPaymentById;
    private readonly IGetPaymentsList _getPaymentsList;
    private readonly ICreatePayment _createPayment;

    public PaymentsController(
        IGetPaymentById getPaymentById,
        IGetPaymentsList getPaymentsList,
        ICreatePayment createPayment)
    {
        _getPaymentById = getPaymentById;
        _getPaymentsList = getPaymentsList;
        _createPayment = createPayment;
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
        var result = await _getPaymentById.ExecuteAsync(id, Guid.Parse("65813c39-48f3-408a-9404-63bb0525a56c"), cancellationToken);
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
        var result = await _getPaymentsList.ExecuteAsync(Guid.Parse("65813c39-48f3-408a-9404-63bb0525a56c"), cancellationToken);

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
    /// <response code="201">Processed payment response</response>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePaymentAsync([FromBody]CreatePaymentDto payment, CancellationToken cancellationToken)
    {
        var result = await _createPayment.ExecuteAsync(
            payment.ConvertToPayment(Guid.Parse("65813c39-48f3-408a-9404-63bb0525a56c")), 
            cancellationToken);
        
        return UseCaseActionResult(result, PaymentDto.CreateFromPayment);
    }
}