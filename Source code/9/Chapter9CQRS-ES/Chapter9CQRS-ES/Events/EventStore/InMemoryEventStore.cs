namespace Chapter9CQRS_API.Events.EventStore;

public class InMemoryEventStore : IEventStore
{
    private readonly List<BaseEvent> events 
        = new List<BaseEvent>();

    public void SaveEvent<T>(T @event) 
        where T : BaseEvent 
        => events.Add(@event);

    public IEnumerable<BaseEvent> GetEvents() 
        => events;
}
