using Chapter12xUnit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Acceptance.Tests;

public class WeatherForecastAcceptanceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public WeatherForecastAcceptanceTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder =>
        {
            // Customize the server or services if needed
            builder.ConfigureServices(services =>
            {
                // Add any test-specific services or overrides here
            });
        }).CreateClient();
    }

    [Fact]
    public async Task GetWeatherForecasts_ReturnsExpectedResults()
    {
        // Arrange
        // Act
        var response = await client.GetAsync("/WeatherForecast");

        // Assert
        response.EnsureSuccessStatusCode();
        var forecasts = await response
            .Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
        Assert.NotNull(forecasts);
        var forecastList = new List<WeatherForecast>(forecasts);
        Assert.Equal(5, forecastList.Count);

        // Additional assertions to verify the correctness of the data
        foreach (var forecast in forecastList)
        {
            Assert.InRange(forecast.TemperatureC, -20, 55);
            Assert.Contains(forecast.Summary, new[] { 
                "Freezing"
                , "Bracing"
                , "Chilly"
                , "Cool"
                , "Mild"
                , "Warm"
                , "Balmy"
                , "Hot"
                , "Sweltering"
                , "Scorching" 
            });
        }
    }

    [Fact]
    public async Task GetWeatherForecasts_ResponseTimeIsAcceptable()
    {
        // Arrange
        var acceptableResponseTime = TimeSpan.FromMilliseconds(2000);

        // Act
        var startTime = DateTime.UtcNow;
        var response = await client.GetAsync("/WeatherForecast");
        var endTime = DateTime.UtcNow;

        // Assert
        response.EnsureSuccessStatusCode();

        // Check response time is within acceptable limits
        var elapsedTime = endTime - startTime;
        Assert.True(elapsedTime < acceptableResponseTime, $"Response time is too slow: {elapsedTime.TotalMilliseconds}ms");
    }
}