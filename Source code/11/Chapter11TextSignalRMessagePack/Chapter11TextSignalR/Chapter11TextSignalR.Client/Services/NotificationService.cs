using System.Text.Json;
using Chapter11TextSignalR.Shared.Models;
using MessagePack;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Chapter11TextSignalR.Client.Services;

public class NotificationService : INotificationService
{
    #region Fields
    private readonly string hubEndpoint = "https://localhost:7273/communicationhub";

    private const string TestEndpoint = "/api/v1/Test";


    /// <summary>
    /// Reconnection timings policy
    /// </summary>
    private readonly TimeSpan[] reconnectionTimeouts =
    {
        TimeSpan.FromSeconds(0),
        TimeSpan.FromSeconds(0),
        TimeSpan.FromSeconds(1),
        TimeSpan.FromSeconds(2),
        TimeSpan.FromSeconds(2),
        TimeSpan.FromSeconds(5),
        TimeSpan.FromSeconds(5),
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(15),
        TimeSpan.FromSeconds(15),
    };
    #endregion

    #region Properties    

    #endregion

    #region Events    
    public event Func<string, Task>? OnMessageReceived;
    #endregion

    public NotificationService()
    {
        _ = InitializeNotifications();
    }

    #region SignalR
    /// <summary>
    /// Gets or sets the hub connection.
    /// </summary>
    /// <value>
    /// The hub connection.
    /// </value>
    public HubConnection? HubConnection { get; set; }

    /// <summary>
    /// Initializes the notifications from machine.
    /// </summary>
    /// <exception cref="InvalidDataException"></exception>
    public async Task InitializeNotifications()
    {
        HubConnection = new HubConnectionBuilder()
            .WithUrl(new Uri("https://localhost:7140/communicationhub"))
            .WithAutomaticReconnect(reconnectionTimeouts)
            .AddMessagePackProtocol()
            .Build();
        _ = HubConnection.On<NotificationTransport>(nameof(IHub.SendMessage), StringMessage);
        await HubConnection.StartAsync();
        await HubConnection.SendAsync(nameof(IHub.AddClientToGroup), "TIME");
    }

    private void GetMyId(object context)
    {
        Console.WriteLine("Messaging hub connection. Arrived: " + context);
    }

    private void StringMessage(NotificationTransport context) 
        => OnMessageReceived?.Invoke(context.Message ?? string.Empty);
    #endregion
}



