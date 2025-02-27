using Chapter9CQRS_API.Features.Commands;
using Chapter9CQRS_API.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
// Get endpoint
app.MapGet("/GetProduct/{id}", async (int id, IMediator mediator) =>
{
    var product = await mediator.Send(new GetProductByIdQuery { Id = id });
    return Results.Ok(product);
})
.WithName("GetProduct")
.WithOpenApi();

// Post endpoint
app.MapPost("/PostProduct", async ([FromBody] CreateProductCommand command, IMediator mediator) =>
{
    var productId = await mediator.Send(command);
    return Results.Ok(productId);
})
.WithName("PostProduct")
.WithOpenApi();

app.Run();
