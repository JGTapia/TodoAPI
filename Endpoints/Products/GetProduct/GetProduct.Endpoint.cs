using FastEndpoints;

using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Endpoints.Products.GetProduct;

public class GetProductEndpoint : Endpoint<GetProductRequest, GetProductResponse>
{
    private readonly TodoDbContext _dbContext;

    public GetProductEndpoint(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductRequest req, CancellationToken ct)
    {
        var product = await _dbContext.Products.FindAsync(new object[] { req.Id }, ct);
        if (product is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new GetProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Manufacturer = product.Manufacturer,
            Stock = product.Stock,
            Price = product.Price
        };

        await SendAsync(response, cancellation: ct);
    }
}