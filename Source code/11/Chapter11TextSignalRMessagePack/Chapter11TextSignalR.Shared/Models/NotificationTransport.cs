namespace Chapter11TextSignalR.Shared.Models;

public class NotificationTransport
{
    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>
    /// The message.
    /// </value>
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the type of the message as string.
    /// </summary>
    /// <value>
    /// The type of the message.
    /// </value>
    public string? MessageType { get; set; }
}
