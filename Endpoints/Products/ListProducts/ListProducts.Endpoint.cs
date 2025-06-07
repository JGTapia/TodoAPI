using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using TodoApi.Data;

namespace TodoApi.Endpoints.Products.ListProducts;

public class ListProductsEndpoint : EndpointWithoutRequest<ListProductsResponse>
{
    private readonly TodoDbContext _dbContext;

    public ListProductsEndpoint(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var products = await _dbContext.Products
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Manufacturer = p.Manufacturer,
                Stock = p.Stock,
                Price = p.Price
            })
            .ToListAsync(ct);

        var response = new ListProductsResponse { Products = products };
        await SendAsync(response, cancellation: ct);
    }
}