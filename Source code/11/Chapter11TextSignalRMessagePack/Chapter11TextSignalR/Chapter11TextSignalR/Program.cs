using Chapter11TextSignalR.Client.Pages;
using Chapter11TextSignalR.Components;
using Chapter11TextSignalR.Models.Hub;
using Chapter11TextSignalR.Models;
using Chapter11TextSignalR.Server.Workers;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Connections;
using MessagePack;
using Chapter11TextSignalR.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();




builder.Services.AddSignalR(
    hubOptions =>
    {
        hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(10);
        hubOptions.MaximumReceiveMessageSize = 65_536;
        hubOptions.HandshakeTimeout = TimeSpan.FromSeconds(1);
        hubOptions.MaximumParallelInvocationsPerClient = 20;
        hubOptions.EnableDetailedErrors = true;
        hubOptions.StreamBufferCapacity = 150;
    })
    .AddMessagePackProtocol(); 


builder.Services.AddSingleton<ICommunicationServer, CommunicationServer>();

builder.Services.AddSingleton<SimpleWorker>();


var app = builder.Build();


// Start the CommunicationServer instance
app.Services.GetRequiredService<ICommunicationServer>().Init("https://localhost:7140/communicationhub");



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Chapter11TextSignalR.Client._Imports).Assembly);

app.MapHub<CommunicationHub>("/communicationhub", options =>
{
    options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
    options.CloseOnAuthenticationExpiration = false;
    options.ApplicationMaxBufferSize = 65_536;
    options.TransportMaxBufferSize = 65_536;
    options.MinimumProtocolVersion = 0;
    options.TransportSendTimeout = TimeSpan.FromSeconds(10);
    options.WebSockets.CloseTimeout = TimeSpan.FromSeconds(3);
});

app.Run();
