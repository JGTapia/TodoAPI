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

        bool isModified = false;
        if (req.Name != string.Empty)
        {
            product.Name = req.Name;
            isModified = true;
        }
        if (req.Description != string.Empty)
        {
            product.Description = req.Description;
            isModified = true;
        }
        if (req.Manufacturer != string.Empty)
        {
            product.Manufacturer = req.Manufacturer;
            isModified = true;
        }
        if (req.Stock != null)
        {
            product.Stock = (int)req.Stock;
            isModified = true;
        }
        if (req.Price != null)
        {
            product.Price = (decimal)req.Price;
            isModified = true;
        }

        if (!isModified)
        {
            await SendNoContentAsync(ct);
            return;
        }

        await _dbContext.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}