namespace Chapter9CQRS_API.Events.EventStore;

public interface IEventStore
{
    void SaveEvent<T>(T @event) where T : BaseEvent;
    IEnumerable<BaseEvent> GetEvents();
}
