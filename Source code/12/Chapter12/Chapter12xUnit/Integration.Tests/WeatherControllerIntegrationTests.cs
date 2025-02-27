using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Reflection;

namespace Chapter12xUnit.Tests;
public class ContactsEndpointsTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;

    public ContactsEndpointsTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
    }

    [Theory]
    [InlineData("/WeatherForecast")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
      Assert.True(response.IsSuccessStatusCode); // Status Code 200-299
    }

    [Fact]
    public async Task GET_retrieves_weather_forecast()
    {
        // Arrange
        using var client = factory.CreateClient();
        // Act
        var response = await client.GetAsync("/weatherforecast");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

}