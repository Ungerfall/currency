using Microsoft.AspNetCore.Mvc;

using Ungerfall.Currency.Application.Services;

namespace Ungerfall.Currency.Presentation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrenciesController : ControllerBase
{
    private readonly ICurrencyService _currencyService;

    public CurrenciesController(ICurrencyService currencyService)
    {
        _currencyService = currencyService ?? throw new System.ArgumentNullException(nameof(currencyService));
    }

    [HttpPost]
    [Route("words")]
    public IActionResult Get(Money money)
    {
        var words = _currencyService.ConvertToWords(money.NumericAmount);
        return Ok(new MoneyInWords { Amount = money.Amount, Words = words });
    }
}
