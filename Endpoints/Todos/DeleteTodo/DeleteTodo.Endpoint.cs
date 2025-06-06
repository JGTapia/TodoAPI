using FastEndpoints;

using TodoApi.Data;

namespace TodoApi.Endpoints.Todos.DeleteTodo;

public class DeleteTodo : Endpoint<DeleteTodoRequest>
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<DeleteTodo> _logger;

    public DeleteTodo(TodoDbContext dbContext, ILogger<DeleteTodo> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override void Configure()
    {
        Delete("/todos/delete/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteTodoRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Deleting todo with ID {TodoId}", req.Id);

        var todo = await _dbContext.Todos.FindAsync(req.Id);

        if (todo is null)
        {
            _logger.LogWarning("Todo with ID {TodoId} not found for deletion", req.Id);
            await SendNotFoundAsync(ct);
            return;
        }

        _dbContext.Todos.Remove(todo);
        await _dbContext.SaveChangesAsync(ct);

        _logger.LogInformation("Deleted todo with ID {TodoId}", req.Id);
        await SendNoContentAsync();
    }
}