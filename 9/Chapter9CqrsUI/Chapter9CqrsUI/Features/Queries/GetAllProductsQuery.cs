using Chapter9CQRS_API.Models;
using MediatR;

namespace Chapter9CQRS_API.Features.Queries;

public class GetAllProductsQuery : IRequest<List<Product>>
{

}
