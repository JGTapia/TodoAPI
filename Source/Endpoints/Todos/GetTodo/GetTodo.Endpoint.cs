using FastEndpoints;

using TodoApi.Data;

namespace TodoApi.Endpoints.Todos.GetTodo;

public class GetTodoEndpoint : Endpoint<GetTodoRequest, GetTodoResponse>
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<GetTodoEndpoint> _logger;

    public GetTodoEndpoint(TodoDbContext dbContext, ILogger<GetTodoEndpoint> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override void Configure()
    {
        Get("/todos/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetTodoRequest req, CancellationToken ct)
    {
        _logger.LogInformation("[GET /todos/id]: Getting todo with ID {TodoId}", req.Id);
        var todo = await _dbContext.Todos.FindAsync(req.Id);

        if (todo is null)
        {
            _logger.LogWarning("Todo with ID {TodoId} not found", req.Id);
            await SendNotFoundAsync(ct);
            return;
        }

        _logger.LogInformation("Returning todo with ID {TodoId}", req.Id);
        await SendAsync(new GetTodoResponse { Title = todo.Title, Done = todo.Done });
    }
}
