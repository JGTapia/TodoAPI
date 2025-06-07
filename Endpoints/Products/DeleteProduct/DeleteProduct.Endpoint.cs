using FastEndpoints;
using TodoApi.Data;

namespace TodoApi.Endpoints.Products.DeleteProduct;

public class DeleteProductEndpoint : Endpoint<DeleteProductRequest>
{
    private readonly TodoDbContext _dbContext;

    public DeleteProductEndpoint(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
    {
        var product = await _dbContext.Products.FindAsync(new object[] { req.Id }, ct);
        if (product is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}