namespace Chapter9CQRS_API.Events.EventStore;

public class ProductCreatedEvent : BaseEvent
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}
