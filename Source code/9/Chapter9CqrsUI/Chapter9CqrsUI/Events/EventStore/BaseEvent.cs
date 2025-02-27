namespace Chapter9CQRS_API.Events.EventStore;

public abstract class BaseEvent
{
    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
}


