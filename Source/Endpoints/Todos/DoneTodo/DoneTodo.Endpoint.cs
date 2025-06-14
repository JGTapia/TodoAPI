using FastEndpoints;

using TodoApi.Data;

namespace TodoApi.Endpoints.Todos.DoneTodo;

public class DoneTodoEndpoint : Endpoint<DoneTodoRequest>
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<DoneTodoEndpoint> _logger;

    public DoneTodoEndpoint(TodoDbContext dbContext, ILogger<DoneTodoEndpoint> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/todos/{id}/done");
        Description(x => x.Accepts<DoneTodoRequest>());
        AllowAnonymous();
    }

    public override async Task HandleAsync(DoneTodoRequest req, CancellationToken ct)
    {
        _logger.LogInformation("[POST /todos/id/done]: Updating todo with ID {TodoId}", req.Id);

        var todo = await _dbContext.Todos.FindAsync(req.Id);

        if (todo is null)
        {
            _logger.LogWarning("[POST /todos/id/done]: Todo with ID {TodoId} not found for Done", req.Id);
            await SendNotFoundAsync(ct);
            return;
        }

        todo.Done = true;

        await _dbContext.SaveChangesAsync(ct);
        _logger.LogInformation("[POST /todos/id/done]: Doned todo");
        await SendNoContentAsync();
    }
}
