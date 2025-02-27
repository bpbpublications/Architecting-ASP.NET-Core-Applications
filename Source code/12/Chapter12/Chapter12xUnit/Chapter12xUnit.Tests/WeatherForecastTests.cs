using Castle.Core.Logging;
using Chapter12xUnit.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TechTalk.SpecFlow;

namespace Chapter12xUnit.Tests;

public class WeatherForecastTests
{
    [Fact]
    public async Task Get_ShouldReturnListOfFiveWeatherForecasts_Success()
    {
        // Arrange
        var logger = new Mock<ILogger<WeatherForecastController>>();
        var controller = new WeatherForecastController(logger.Object);
        // Act
        var result = controller.Get();
        // Assert
        Assert.Equal(5, result.Count());
    }



}


