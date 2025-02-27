namespace Chapter11TextSignalR.Shared.Models;

public interface IHub
{
    Task SendMessage(object message);
    Task GetId(object id);
    Task SendToGroup(string groupName, NotificationTransport message);
    Task AddClientToGroup(string groupName);
    Task RemoveClientToGroup(string groupName);
}

