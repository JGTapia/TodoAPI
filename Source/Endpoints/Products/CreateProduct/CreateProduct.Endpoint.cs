using FastEndpoints;

using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Endpoints.Products.CreateProduct;

public class CreateProductEndpoint : Endpoint<CreateProductRequest, CreateProductResponse>
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<CreateProductEndpoint> _logger;

    public CreateProductEndpoint(TodoDbContext dbContext, ILogger<CreateProductEndpoint> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/products");
        Validator<CreateProductValidator>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateProductRequest req, CancellationToken ct)
    {
        _logger.LogInformation("[POST /products]: Creating product {@Product}", req);

        var product = new Product
        {
            Name = req.Name,
            Description = req.Description,
            Manufacturer = req.Manufacturer,
            Stock = req.Stock,
            Price = req.Price
        };

        _dbContext.Add(product);
        await _dbContext.SaveChangesAsync(ct);

        var response = new CreateProductResponse { Id = product.Id };
        await SendAsync(response, statusCode: 201);
    }
}