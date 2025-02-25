using Chapter9CQRS_API.Models;
using MediatR;

namespace Chapter9CQRS_API.Features.Queries;

public class GetProductByIdQuery : IRequest<Product>
{
    public int Id { get; set; }
}
