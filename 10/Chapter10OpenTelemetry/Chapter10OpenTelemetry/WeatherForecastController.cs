using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> logger;
    private readonly Tracer tracer;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, TracerProvider tracerProvider)
    {
        this.logger = logger;
        tracer = tracerProvider.GetTracer(nameof(WeatherForecastController));
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        using (var span = tracer.StartActiveSpan(nameof(Get)))
        {
            logger.LogInformation("Handling Get request for WeatherForecast");
            var rng = new Random();
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            logger.LogInformation("WeatherForecast generated successfully");
            return forecasts;
        }
    }
}

public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }
}
