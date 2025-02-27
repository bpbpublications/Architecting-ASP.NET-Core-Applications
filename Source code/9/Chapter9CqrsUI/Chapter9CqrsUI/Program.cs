using Chapter9CQRS_API.Events.EventStore;
using Chapter9CQRS_API.Features.Commands;
using Chapter9CQRS_API.Features.Queries;
using Chapter9CQRS_API.Models;
using Chapter9CQRS_API.Projections;
using Chapter9CqrsUI.Components;
using Chapter9CqrsUI.HttpServices;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("MyApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7223/");
});

builder.Services.AddMediatR(cfg => cfg
    .RegisterServicesFromAssembly(
        typeof(Program).Assembly));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:7223")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddSingleton<IEventStore, InMemoryEventStore>();

builder.Services.AddSingleton<ProductsProjection>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();



//// Get endpoint
//app.MapGet("/GetProduct/{id}", async (int id, IMediator mediator) =>
//{
//    var product = await mediator.Send(new GetProductByIdQuery { Id = id });
//    return Results.Ok(product);
//})
//.WithName("GetProduct")
//.WithOpenApi();

//// Post endpoint for product creation
//app.MapPost("/PostProduct", async ([FromBody] Product product, IMediator mediator) =>
//{
//    CreateProductCommand command = new CreateProductCommand()
//    {
//        Name = product.Name,
//        Price = product.Price,
//    };
//    var productId = await mediator.Send(command);
//    return Results.Ok(productId);
//})
//.WithName("PostProduct")
//.WithOpenApi();

//// GetProductList
//app.MapGet("/GetProductList", async (ProductsProjection projection) =>
//{
//    var products = projection.GetAll();

//    //var products = await mediator.Send(new GetAllProductsQuery());
//    return Results.Ok(products);
//})
//.WithName("GetProductList")
//.WithOpenApi();

app.UseApi();

app.Run();

public static class ConfigurationsController
{
    public static IEndpointRouteBuilder UseApi(
    this IEndpointRouteBuilder app)
    {
        _ = app.MapGroup("")
               .MapApi();
        return app;
    }

    /// <summary>
    /// Map the endpoints with the APIs.
    /// </summary>
    /// <param name="group">The route group.</param>
    /// <param name="config">The api configuration instance.</param>
    [ExcludeFromCodeCoverage]
    private static RouteGroupBuilder MapApi(this RouteGroupBuilder group)
    {
        _ = group.MapGet($"/GetProduct/{{id}}", GetProduct)
            .WithName("GetProduct");
        _ = group.MapPost($"/PostProduct", PostProduct)
                .WithName("PostProduct");
        _ = group.MapGet($"/GetProductList", GetProductList)
                .WithName("GetProductList");
        _ = group.MapPost($"/UpdateProductPrice", UpdateProductPrice)
                .WithName("UpdateProductPrice");
        return group;
    }

    private static async Task<IResult> GetProduct(int id, [FromServices] IMediator mediator)
    {
        var product = await mediator.Send(new GetProductByIdQuery { Id = id });
        return Results.Ok(product);
    }

    private static async Task<IResult> PostProduct(Product product, [FromServices] IMediator mediator)
    {
        CreateProductCommand command = new CreateProductCommand()
        {
            Name = product.Name,
            Price = product.Price,
        };
        var productId = await mediator.Send(command);
        return Results.Ok(productId);
    }


    private static async Task<IResult> GetProductList(ProductsProjection projection, [FromServices] IMediator mediator)
    {
        await Task.Yield();
        var products = projection.GetAll();
        var ppp = await mediator.Send(new GetAllProductsQuery());
        return Results.Ok(products?.Values.ToList());
    }

    private static async Task<IResult> UpdateProductPrice([FromBody] UpdateProductPriceCommand command, [FromServices] IMediator mediator)
    {
        var updatePrice = new UpdateProductPriceCommand()
        {
            Id = command.Id,
            Price = command.Price,
        };
        var productPrice = await mediator.Send(command);
        return Results.Ok(productPrice);
    }
}