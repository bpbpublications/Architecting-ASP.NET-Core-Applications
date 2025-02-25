using NBomber.CSharp;
using var httpClient = new HttpClient();
var scenario = Scenario.Create("weather_forecast_scenario"
    , async context =>
{
    var response = await httpClient
        .GetAsync("https://localhost:7052/weatherforecast");
    return response.IsSuccessStatusCode
        ? Response.Ok()
        : Response.Fail();
})
.WithoutWarmUp()
.WithLoadSimulations(
    Simulation.Inject(rate: 10,
        interval: TimeSpan.FromSeconds(1),
        during: TimeSpan.FromSeconds(30))
);

NBomberRunner
    .RegisterScenarios(scenario)
    .Run();