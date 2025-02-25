using MediatR;

namespace Chapter9CQRS_API.Features.Commands;

public class CreateProductHandler
    : IRequestHandler<CreateProductCommand, int>
{
    public Task<int> Handle(
        CreateProductCommand request
        , CancellationToken cancellationToken)
    {
        // Here you would add your logic
        // to save the product to the database
        // For simplicity, let's assume
        // we're returning a random ID
        var newId = Random.Shared.Next(1, 10000);
        return Task.FromResult(newId);
    }
}

