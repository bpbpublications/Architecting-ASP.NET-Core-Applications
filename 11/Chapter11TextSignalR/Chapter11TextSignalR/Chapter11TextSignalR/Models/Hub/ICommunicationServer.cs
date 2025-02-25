namespace Chapter11TextSignalR.Models.Hub;

public interface ICommunicationServer
{
    /// <summary>
    /// Sends the channel activation message.
    /// </summary>
    void SendChannelActivationMessage();

    /// <summary>
    /// Sends the specified message.
    /// </summary>
    /// <param name="toSend">To send.</param>
    void Send(object toSend);

    /// <summary>
    /// Initializes the specified address.
    /// </summary>
    /// <param name="address">The address.</param>
    void Init(string address);
}
