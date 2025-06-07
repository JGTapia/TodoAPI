using FastEndpoints;

using TodoApi.Data;

namespace TodoApi.Endpoints.Products.UpdateProduct;

public class UpdateProductEndpoint : Endpoint<UpdateProductRequest>
{
    private readonly TodoDbContext _dbContext;

    public UpdateProductEndpoint(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/products/{id}");
        Validator<UpdateProductValidator>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
    {
        var product = await _dbContext.Products.FindAsync(new object[] { req.Id }, ct);
        if (product is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        product.Name = req.Name;
        product.Description = req.Description;
        product.Manufacturer = req.Manufacturer;
        product.Stock = req.Stock;
        product.Price = req.Price;

        await _dbContext.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}