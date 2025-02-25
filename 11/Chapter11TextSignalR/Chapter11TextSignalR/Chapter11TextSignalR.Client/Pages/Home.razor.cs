using Chapter11TextSignalR.Client.Services;
using Chapter11TextSignalR.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Chapter11TextSignalR.Client.Pages;

public partial class Home
{
    private string Message = string.Empty;
    private string connectionId = string.Empty;

    private NotificationService? notificationService;

    HubConnection hubConnection1 = new HubConnectionBuilder()
        .WithUrl(new Uri("https://localhost:7140/communicationhub"))
        .Build();

    protected override async Task OnInitializedAsync()
    {
        await Client1();
    }

    private void GetMyId(object context)
    {
        Console.WriteLine("Messaging hub connection. Arrived: " + context);
        connectionId = context.ToString();
        Dispatcher.CreateDefault().InvokeAsync(StateHasChanged);
    }

    private async Task Client1()
    {
        _ = hubConnection1.On<string>(nameof(IHub.GetId), GetMyId);

        hubConnection1.On<NotificationTransport>(nameof(IHub.SendMessage), message =>
        {
            Message = message.Message ?? string.Empty;
            Dispatcher.CreateDefault().InvokeAsync(StateHasChanged);
        });
        await hubConnection1.StartAsync();

        // Add to group
        await hubConnection1.SendAsync("AddClientToGroup", "TIME");
    }
}
