using MediatR;

namespace Chapter9CQRS_API.Features.Commands;

public class UpdateProductPriceCommand : IRequest<decimal>
{
    public int Id { get; set; }
    public decimal Price { get; set; }
}
