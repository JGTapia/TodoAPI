using FastEndpoints;

using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Endpoints.TodosCompact.Commands;

public class CreateTodoRequest
{
    public string Title { get; set; } = string.Empty;
    public bool Done { get; set; }
}

public class CreateTodoResponse
{
    public int Id { get; set; }
}

public class CreateTodo : Endpoint<CreateTodoRequest, CreateTodoResponse>
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<CreateTodo> _logger;



    public CreateTodo(TodoDbContext dbContext, ILogger<CreateTodo> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/todoscompact/create");
        Validator<CreateTodoValidator>();
        AllowAnonymous();

        // Handle Validation Errors Manually
        //DontThrowIfValidationFails();
    }

    public override async Task HandleAsync(CreateTodoRequest req, CancellationToken ct)
    {
        // Handle Validation Errors Manually
        // if (ValidationFailed)
        // {
        //     foreach (ValidationFailure failure in ValidationFailures)
        //     {
        //         var propertyName = failure.PropertyName;
        //         var errorMessage = failure.ErrorMessage;

        //         AddError(r => propertyName, errorMessage);

        //     }
        //     ThrowIfAnyErrors(); // If there are errors, execution shouldn't go beyond this point
        // }

        _logger.LogInformation("Creating new todo with title {TodoTitle}", req.Title);

        var todo = new Todo
        {
            Title = req.Title,
            Done = req.Done
        };

        _dbContext.Todos.Add(todo);
        await _dbContext.SaveChangesAsync(ct);

        var response = new CreateTodoResponse { Id = todo.Id };
        await SendAsync(response, statusCode: 201);
    }
}

public class CreateTodoValidator : Validator<CreateTodoRequest>
{
    public CreateTodoValidator()
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