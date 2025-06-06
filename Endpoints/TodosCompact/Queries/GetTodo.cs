using FastEndpoints;

using TodoApi.Data;

namespace TodoApi.Endpoints.TodosCompact.Queries;

public class GetTodoRequest
{
    public int Id { get; set; }
}

public class GetTodoResponse

{
    public string Title { get; set; } = string.Empty;
    public bool Done { get; set; }
}

public class GetTodo : Endpoint<GetTodoRequest, GetTodoResponse>
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<GetTodo> _logger;

    public GetTodo(TodoDbContext dbContext, ILogger<GetTodo> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override void Configure()
    {
        Get("/todoscompact/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetTodoRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Getting todo with ID {TodoId}", req.Id);
        var todo = await _dbContext.Todos.FindAsync(req.Id);

        if (todo is null)
        {
            _logger.LogWarning("Todo with ID {TodoId} not found", req.Id);
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new GetTodoResponse
        {
            Title = todo.Title,
            Done = todo.Done
        };
        await SendAsync(response);
    }
}
