using MediatR;

namespace Chapter9CQRS_API.Features.Commands;

public class CreateProductCommand : IRequest<int>
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
}

