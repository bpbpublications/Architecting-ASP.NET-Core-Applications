namespace Chapter9CQRS_API.Events.EventStore;

public class ProductPriceUpdatedEvent : BaseEvent
{
    public int ProductId { get; set; }
    public decimal NewPrice { get; set; }
}