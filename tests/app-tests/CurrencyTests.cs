using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Ungerfall.Currency.Presentation.WebApi.Tests;

public class CurrencyTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _outputHelper;

    public CurrencyTests(
        WebApplicationFactory<Program> factory,
        ITestOutputHelper outputHelper)
    {
        _factory = factory;
        _outputHelper = outputHelper;
    }

    [Theory]
    [InlineData("0", "zero dollars")]
    [InlineData("1", "one dollar")]
    [InlineData("25,1", "twenty-five dollars and ten cents")]
    [InlineData("0,01", "zero dollars and one cent")]
    [InlineData("45 100", "forty-five thousand one hundred dollars")]
    [InlineData("999 999 999,99", "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
    public async Task CurrencyToWords_ShouldReturnExpected(string amount, string expectedWords)
    {
        // Arrange
        var client = _factory.CreateClient();
        var money = new Money { Amount = amount };

        // Act
        var response = await client.PostAsync(
            "/api/currencies/words",
            JsonContent.Create(money, typeof(Money)));

        // Assert
        var words = await response.Content.ReadFromJsonAsync<MoneyInWords>();
        response.EnsureSuccessStatusCode();
        Assert.NotNull(words);
        Assert.Equal(expectedWords, words?.Words);
    }

    [Theory]
    [InlineData("-1,91")]
    [InlineData("10.33")]
    [InlineData("1 000 000 000,22")]
    [InlineData("0,991")]
    public async Task CurrencyToWords_InvalidFormat_ShouldReturnBadRequest(string amount)
    {
        // Arrange
        var client = _factory.CreateClient();
        var money = new Money { Amount = amount };

        // Act
        var response = await client.PostAsync(
            "/api/currencies/words",
            JsonContent.Create(money, typeof(Money)));

        // Assert
        _outputHelper.WriteLine(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}