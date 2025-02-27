using Chapter11TextSignalR.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


//builder.Services.AddScoped<INotificationService, NotificationService>();


await builder.Build().RunAsync();