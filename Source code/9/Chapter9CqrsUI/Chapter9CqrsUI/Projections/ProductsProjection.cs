using Chapter9CQRS_API.Events.EventStore;
using Chapter9CQRS_API.Models;

namespace Chapter9CQRS_API.Projections;

public class ProductsProjection
{
    private readonly Dictionary<int, Product> readModel
        = new Dictionary<int, Product>();

    public IEventStore EventStore { get; }

    public ProductsProjection(IEventStore eventStore)
    {
        EventStore = eventStore;
        ExtractEvents(eventStore);
    }

    private void ExtractEvents(IEventStore eventStore)
    {
        var events = eventStore.GetEvents();
        foreach (var @event in events)
        {
            Apply(@event);
        }
    }

    private void Apply(BaseEvent @event)
    {
        switch (@event)
        {
            case ProductCreatedEvent e:
                readModel[e.ProductId] = new Product
                {
                    Id = e.ProductId,
                    Name = e.Name,
                    Price = e.Price
                };
                break;
            case ProductPriceUpdatedEvent e:
                readModel.TryGetValue(e.ProductId, out var product);
                if (product is not null)
                {
                    product.Price = e.NewPrice;
                }
                break;
        }
    }

    public Dictionary<int, Product> GetAll()
    {
        ExtractEvents(EventStore);
        return readModel;
    }

    internal Product GetProductById(int id)
    {
        ExtractEvents(EventStore);
        var product = readModel[id];
        return product;
    }
}

