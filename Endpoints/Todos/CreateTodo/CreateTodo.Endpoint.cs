using FastEndpoints;

using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Endpoints.Todos.CreateTodo;

public class CreateTodoEndpoint : Endpoint<CreateTodoRequest, CreateTodoResponse>
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<CreateTodoEndpoint> _logger;

    public CreateTodoEndpoint(TodoDbContext dbContext, ILogger<CreateTodoEndpoint> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/todos/create");
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


        _logger.LogInformation("Added new todo with title {TodoTitle}", req.Title);
        var response = new CreateTodoResponse { Id = todo.Id };
        await SendAsync(response, statusCode: 201);
    }
}
