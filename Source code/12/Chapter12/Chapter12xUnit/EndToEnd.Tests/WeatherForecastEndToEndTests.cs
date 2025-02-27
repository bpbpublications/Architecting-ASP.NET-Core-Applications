using Chapter12xUnit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace EndToEnd.Tests;

public class WeatherForecastEndToEndTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public WeatherForecastEndToEndTests
        (WebApplicationFactory<Program> factory)
            => client = factory.CreateClient();

    [Fact]
    public async Task GetWeatherForecasts_ReturnsExpectedResults()
    {
        // Act
        var response = await client.GetAsync("/WeatherForecast");

        // Assert
        response.EnsureSuccessStatusCode();
        var forecasts = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
        Assert.NotNull(forecasts);
        var forecastList = new List<WeatherForecast>(forecasts);
        Assert.Equal(5, forecastList.Count);
    }
}
