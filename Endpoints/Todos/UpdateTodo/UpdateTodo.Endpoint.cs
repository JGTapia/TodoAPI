using FastEndpoints;

using TodoApi.Data;

namespace TodoApi.Endpoints.Todos.UpdateTodo;

public class UpdateTodoEndpoint : Endpoint<UpdateTodoRequest>
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<UpdateTodoEndpoint> _logger;

    public UpdateTodoEndpoint(TodoDbContext dbContext, ILogger<UpdateTodoEndpoint> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override void Configure()
    {
        Put("/todos/{id}");
        Validator<UpdateTodoValidator>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateTodoRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Updating todo with ID {TodoId}", req.Id);

        var todo = await _dbContext.Todos.FindAsync(req.Id);

        if (todo is null)
        {
            _logger.LogWarning("Todo with ID {TodoId} not found for update", req.Id);
            await SendNotFoundAsync(ct);
            return;
        }

        if (req.Title != string.Empty)
            todo.Title = req.Title;
        todo.Done = req.Done;

        await _dbContext.SaveChangesAsync(ct);
        _logger.LogInformation("Updated todo with ID {TodoId}", req.Id);
        await SendNoContentAsync();
    }
}
