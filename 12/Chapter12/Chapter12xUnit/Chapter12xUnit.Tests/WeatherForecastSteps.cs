using Chapter12xUnit.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using TechTalk.SpecFlow;

namespace Chapter12xUnit.Tests;

[Binding]
public class WeatherForecastSteps
{
    private WeatherForecastController controller;
    private IEnumerable<WeatherForecast> result;

    [Given(@"I have a weather forecast controller")]
    public void GivenIHaveAWeatherForecastController()
    {
        var logger = new Mock<ILogger<WeatherForecastController>>();
        controller = new WeatherForecastController(logger.Object);
    }

    [When(@"I request the weather forecast")]
    public void WhenIRequestTheWeatherForecast()
    {
        result = controller.Get();
    }

    [Then(@"I should receive a list of (.*) weather forecasts")]
    public void ThenIShouldReceiveAListOfWeatherForecasts(int count)
    {
        Assert.Equal(count, result.Count());
    }
}
