using Microsoft.AspNetCore.Mvc;

namespace CheckoutGateway.Api.Controllers;

[Route("api/transactions")]
[ApiController]
public class TransactionsController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult Index()
    {
        return Ok("hi");
    }
}