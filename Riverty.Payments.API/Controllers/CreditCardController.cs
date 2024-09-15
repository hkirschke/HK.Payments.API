using HK.Payments.Core.Models;
using HK.Payments.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HK.Payments.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CreditCardController : ControllerBase
{
    private readonly ICreditCardService _creditCardService;

    public CreditCardController(ICreditCardService creditCardService)
    {
        _creditCardService = creditCardService;
    }

    [HttpPost("validate")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<string>> CreditCardValidationAsync([FromBody] CreditCardModel creditCard)
    {
        var result = await _creditCardService.CreditCardValidationAsync(creditCard);

        return Created(string.Empty, result);
    }
}