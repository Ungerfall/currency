using System.ComponentModel.DataAnnotations;

using Humanizer;

namespace Ungerfall.Currency.Application.Services;

public sealed class CurrencyService : ICurrencyService
{
    private const int MAX_UNITS = 999_999_999;
    private const decimal MAX_FRACTIONS = 0.99m;

    public string ConvertToWords(decimal amount)
    {
        if (amount < 0m)
        {
            throw new ValidationException("Negative amount");
        }

        var units = decimal.Truncate(amount);
        decimal fractions = amount % 1.0m;
        if (units > int.MaxValue || units > MAX_UNITS)
        {
            throw new ValidationException($"Maximum number of units is {MAX_UNITS}");
        }

        if (fractions > MAX_FRACTIONS)
        {
            throw new ValidationException($"Maximum number of fractions is {MAX_FRACTIONS}");
        }

        int unitsInt = (int)units;
        int fractionsInt = (int)decimal.Truncate(fractions * 100);

        var words = unitsInt
            .ToWords()
            .WithDollars(unitsInt)
            .WithCents(fractionsInt);

        return words;
    }
}