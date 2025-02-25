using Chapter9CQRS_API.Events.EventStore;
using Chapter9CQRS_API.Events;
using MediatR;

namespace Chapter9CQRS_API.Features.Commands;

public class CreateProductHandler 
    : IRequestHandler<CreateProductCommand, int>
{
    private readonly IEventStore eventStore;

    public CreateProductHandler(IEventStore eventStore) 
        => this.eventStore = eventStore;

    public Task<int> Handle(
        CreateProductCommand request
        , CancellationToken cancellationToken)
    {
        var productId = Random.Shared.Next(1, 10000);
        var productCreatedEvent = new ProductCreatedEvent
        {
            ProductId = productId,
            Name = request.Name,
            Price = request.Price
        };

        eventStore.SaveEvent(productCreatedEvent);
        return Task.FromResult(productId);
    }
}
