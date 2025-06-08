using FastEndpoints;

using TodoApi.Data;

namespace TodoApi.Endpoints.TodosCompact.Commands;

public class UpdateTodoRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool Done { get; set; }
}

public class UpdateTodoValidator : Validator<UpdateTodoRequest>
{
    public UpdateTodoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("your title is required!")
            .MinimumLength(3)
            .WithMessage("your title is too short!")
            .MaximumLength(20)
            .WithMessage("your title is too long!");
    }
}

public class UpdateTodo : Endpoint<UpdateTodoRequest>
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<UpdateTodo> _logger;

    public UpdateTodo(TodoDbContext dbContext, ILogger<UpdateTodo> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override void Configure()
    {
        Put("/todoscompact/{id}");
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
        await SendNoContentAsync();
    }
}
