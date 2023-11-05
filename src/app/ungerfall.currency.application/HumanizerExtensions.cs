using Humanizer;

namespace Ungerfall.Currency.Application;
public static class HumanizerExtensions
{
    public static string WithDollars(this string words, int dollars)
    {
        return words.RemoveAnd() + " " + (dollars == 1 ? "dollar" : "dollars");
    }

    public static string WithCents(this string words, int cents)
    {
        if (cents == 0)
        {
            return words;
        }

        return words + " and " + cents.ToWords().RemoveAnd() + " "
            + (cents == 1 ? "cent" : "cents");
    }

    private static string RemoveAnd(this string words)
    {
        return words.Replace(" and", string.Empty);
    }
}
