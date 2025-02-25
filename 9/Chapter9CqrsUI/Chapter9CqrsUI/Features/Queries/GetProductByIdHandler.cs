using Chapter9CQRS_API.Models;
using Chapter9CQRS_API.Projections;
using MediatR;

namespace Chapter9CQRS_API.Features.Queries;

public class GetProductByIdHandler(ProductsProjection projection) : IRequestHandler<GetProductByIdQuery, Product>
{
    public ProductsProjection Projection { get; } = projection;

    public Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        // Here you would retrieve the product from the database
        // For simplicity, let's return a static product

        //var product = new Product
        //{
        //    Id = request.Id,
        //    Name = "Example Product",
        //    Price = 99.99M
        //};

        Product product = Projection.GetProductById(request.Id);

        return Task.FromResult(product);
    }
}