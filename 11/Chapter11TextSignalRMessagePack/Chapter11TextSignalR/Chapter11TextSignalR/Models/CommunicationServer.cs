using Chapter11TextSignalR.Models.Hub;
using Chapter11TextSignalR.Server.Workers;
using Chapter11TextSignalR.Shared.Models;
using MessagePack;
using Microsoft.AspNetCore.SignalR.Client;

namespace Chapter11TextSignalR.Models;

public class CommunicationServer : ICommunicationServer
{
    SimpleWorker worker;
    string apiKey = string.Empty;
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    #region Constructor
    public CommunicationServer(SimpleWorker worker, IConfiguration configuration)//, NewsApiKey apiKey)
    {
        this.worker = worker;
        apiKey = configuration["NewsApiKey"];
    }
    #endregion

    #region Fields

    private HubConnection? hubConnection;

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

    #region Methods and callbacks
    /// <summary>
    /// Initializes the SignalR connection with a configuration base address.
    /// </summary>
    /// <param name="baseAddress">The base address.</param>
    public async void Init(string baseAddress)
    {
        hubConnection = new HubConnectionBuilder()
                       .WithUrl(new Uri(baseAddress))
                       .WithAutomaticReconnect(reconnectionTimeouts)
                       .AddMessagePackProtocol()
                       .Build();
        await hubConnection.StartAsync();
        worker.SetHub(hubConnection, apiKey, baseAddress);
        worker.ExecuteAsync(cancellationTokenSource.Token);
        await worker.GetNews(cancellationTokenSource.Token);
    }

    /// <inheritdoc cref="ICommunicationServer"/>>
    public void Send(object toSend) => hubConnection?.SendAsync(nameof(IHub.SendMessage), toSend);

    /// <inheritdoc cref="ICommunicationServer"/>>
    public void SendChannelActivationMessage() => Send(new NotificationTransport { Message = "Initialization message", MessageType = "none" });

    #endregion
}
