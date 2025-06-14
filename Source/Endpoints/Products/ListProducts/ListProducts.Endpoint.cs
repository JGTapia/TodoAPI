using System.Security.Claims;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using TodoApi.Data;

namespace TodoApi.Endpoints.Products.ListProducts;


public class ListProductsEndpoint : Endpoint<ListProductsRequest, ListProductsResponse>
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<ListProductsEndpoint> _logger;

    public ListProductsEndpoint(TodoDbContext dbContext, ILogger<ListProductsEndpoint> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();

        //Roles("Administrators");

    }

    public override async Task HandleAsync(ListProductsRequest req, CancellationToken ct)
    {
        _logger.LogInformation("[GET /products]: Get products with Page {Page} and PageSize {PageSize}", req.Page, req.PageSize);

        // var userName = HttpContext.User.Identity?.Name; // Get the authenticated user's name
        // var userRoles = HttpContext.User.Claims
        //     .Where(c => c.Type == ClaimTypes.Role)
        //     .Select(c => c.Value); // Get the user's roles

        // _logger.LogWarning("Request made by user: {UserName}", userName);
        // _logger.LogWarning("User roles: {UserRoles}", string.Join(", ", userRoles));


        var page = (req.Page ?? 1) != 0 ? req.Page ?? 1 : 1;
        var pageSize = (req.PageSize ?? 10) != 0 ? req.PageSize ?? 10 : 10;
        // Use null-coalescing operators to provide default values for page and pageSize
        var products = await _dbContext.Products
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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