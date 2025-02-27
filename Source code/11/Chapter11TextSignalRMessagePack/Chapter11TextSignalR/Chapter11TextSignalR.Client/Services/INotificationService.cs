using Microsoft.AspNetCore.SignalR.Client;

namespace Chapter11TextSignalR.Client.Services;
public interface INotificationService
{
    HubConnection? HubConnection { get; set; }

    event Func<string, Task> OnMessageReceived;

}