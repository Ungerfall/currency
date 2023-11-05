using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Ungerfall.Currency.Presentation.WebApi;

public class Money
{
    [Required]
    [NotNull]
    public string Amount { get; set; }

    [JsonIgnore]
    public decimal NumericAmount
    {
        get
        {
            var nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = ",",
                NumberGroupSeparator = " "
            };
            if (!decimal.TryParse(Amount, NumberStyles.Number, nfi, out decimal amount))
            {
                throw new ValidationException($"Input amount ({Amount}) was in incorrect format. Number separator is comma \",\"");
            }

            return amount;
        }
    }
}
