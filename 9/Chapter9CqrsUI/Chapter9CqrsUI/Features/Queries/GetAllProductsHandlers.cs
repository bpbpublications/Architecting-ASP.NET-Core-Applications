using Chapter9CQRS_API.Events.EventStore;
using Chapter9CQRS_API.Models;
using Chapter9CQRS_API.Projections;
using MediatR;

namespace Chapter9CQRS_API.Features.Queries;

public class GetAllProductsHandlers : IRequestHandler<GetAllProductsQuery, List<Product>>
{
    public ProductsProjection Projection { get; }

    public GetAllProductsHandlers(ProductsProjection projection)
    {
        Projection = projection;
    }



    public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var products = new List<Product>();
        var pro = Projection.GetAll();
        products = pro.Values.ToList();
        return products;
    }
}
