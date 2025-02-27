using Chapter9CQRS_API.Events.EventStore;
using MediatR;

namespace Chapter9CQRS_API.Features.Commands;

public class UpdateProductPriceHandler
    : IRequestHandler<UpdateProductPriceCommand, decimal>
{
    private readonly IEventStore eventStore;

    public UpdateProductPriceHandler(IEventStore eventStore)
        => this.eventStore = eventStore;

    public Task<decimal> Handle(
        UpdateProductPriceCommand request
        , CancellationToken cancellationToken)
    {
        var productPriceUpdateEvent = new ProductPriceUpdatedEvent
        {
            ProductId = request.Id,
            NewPrice = request.Price
        };

        eventStore.SaveEvent(productPriceUpdateEvent);
        return Task.FromResult(request.Price);
    }
}
