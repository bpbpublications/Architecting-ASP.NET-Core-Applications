using Chapter9CQRS_API.Events.EventStore;
using Chapter9CQRS_API.Models;

namespace Chapter9CQRS_API.Projections;

public class ProductsProjection
{
    private readonly Dictionary<int, Product> readModel
        = new Dictionary<int, Product>();

    public ProductsProjection(IEventStore eventStore)
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
}

