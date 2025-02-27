using Chapter11TextSignalR.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chapter11TextSignalR.Models.Hub;

public class CommunicationHub : Hub<IHub>
{
    public static List<string> ClientsList { get; set; } = new List<string>();

    /// <summary>
    /// Messages the specified message.
    /// </summary>
    /// <param name="message">The message.</param>
    public async Task Message(object message) => await Clients.All.SendMessage(message);

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    public async Task GetId(object id) => await Clients.Caller.GetId(Context.ConnectionId);


    #region GROUPS    
    /// <summary>
    /// Sends to group.
    /// </summary>
    /// <param name="groupName">Name of the group.</param>
    /// <param name="message">The message.</param>
    public async Task SendToGroup(string groupName, NotificationTransport message) => await Clients.Group(groupName).SendMessage(message);

    /// <summary>
    /// Adds the client to group.
    /// </summary>
    /// <param name="groupName">Name of the group.</param>
    public async Task AddClientToGroup(string groupName) => await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

    /// <summary>
    /// Removes the client to group.
    /// </summary>
    /// <param name="groupName">Name of the group.</param>
    public async Task RemoveClientToGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        await Clients.Client(Context.ConnectionId).SendMessage("Removed from group");
    }
    #endregion

    /// <summary>
    /// Called when a new connection is established with the hub.
    /// </summary>
    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.GetId(Context.ConnectionId.ToString());
        ClientsList.Add(Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    /// <summary>
    /// Called when a connection with the hub is terminated.
    /// </summary>
    /// <param name="exception"></param>
    /// <returns>
    /// A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous disconnect.
    /// </returns>
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        ClientsList.Remove(Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
}
