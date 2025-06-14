using FastEndpoints;

using TodoApi.Data;

namespace TodoApi.Endpoints.Products.DeleteProduct;

public class DeleteProductEndpoint : Endpoint<DeleteProductRequest>
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<DeleteProductEndpoint> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeleteProductEndpoint(
        TodoDbContext dbContext,
        ILogger<DeleteProductEndpoint> logger,
        IHttpClientFactory httpClientFactory,
        IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    public override void Configure()
    {
        Delete("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
    {
        //var correlationId = _httpContextAccessor.HttpContext?.Items["X-Correlation-ID"]?.ToString();

        // _logger.LogInformation("Deleting product '{Id}' with CorrelationId={CorrelationId}", req.Id, correlationId);

        var product = await _dbContext.Products.FindAsync(new object[] { req.Id }, ct);
        if (product is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // // 🌐 Call external service with the correlation ID
        // var client = _httpClientFactory.CreateClient();
        // var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://external-service/api/notify")
        // {
        //     Content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json")
        // };
        // if (!string.IsNullOrWhiteSpace(correlationId))
        //     requestMessage.Headers.Add("X-Correlation-ID", correlationId);

        // var response = await client.SendAsync(requestMessage, ct);
        // _logger.LogInformation("External service responded with {StatusCode}", response.StatusCode);



        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}