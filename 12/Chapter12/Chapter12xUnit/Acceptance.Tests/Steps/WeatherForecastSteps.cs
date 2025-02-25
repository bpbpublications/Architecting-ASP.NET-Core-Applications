using Chapter12xUnit;
using Microsoft.AspNetCore.Mvc.Testing;
using TechTalk.SpecFlow;

namespace Acceptance.Tests.Steps;

[Binding]
public class WeatherForecastSteps : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;
    private HttpClient client;
    private HttpResponseMessage? response;
    private IEnumerable<WeatherForecast>? forecasts;

    public WeatherForecastSteps(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
        client = factory.CreateClient();
    }

    [Given("the weather forecast service is running")]
    public void GivenTheWeatherForecastServiceIsRunning()
    {
        // No setup needed,
        // the factory ensures
        // the service is running
    }

    [When("I request the weather forecast")]
    public async Task WhenIRequestTheWeatherForecast()
    {
        response = await client.GetAsync("/WeatherForecast");
        if (response is null) return;
        if (response.Content is null) return;
        forecasts = await response.Content
            .ReadFromJsonAsync<IEnumerable<WeatherForecast>>()
            ?? throw new Exception();
    }

    [Then("I should receive 5 forecasts")]
    public void ThenIShouldReceive5Forecasts()
    {
        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts.Count());
    }

    [Then("each forecast should have a date, temperature, and summary")]
    public void ThenEachForecastShouldHaveADateTemperatureAndSummary()
    {
        foreach (var forecast in forecasts)
        {
            Assert.NotNull(forecast.Date);
            Assert.NotNull(forecast.Summary);
        }
    }

    [Then("the temperature should be between -20 and 55 degrees")]
    public void ThenTheTemperatureShouldBeBetweenAndDegrees()
    {
        foreach (var forecast in forecasts)
        {
            Assert.InRange(forecast.TemperatureC, -20, 55);
        }
    }

    [Then("the summary should be one of the predefined summaries")]
    public void ThenTheSummaryShouldBeOneOfThePredefinedSummaries()
    {
        var summaries = new[]{
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
            };
        foreach (var forecast in forecasts)
        {
            Assert.Contains(forecast.Summary, summaries);
        }
    }
}

