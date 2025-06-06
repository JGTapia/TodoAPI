using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using TodoApi.Data;

namespace TodoApi.Endpoints.TodosCompact.Queries;

public class ListTodosResponse
{
    public List<TodoItem>? Items { get; set; }
    public int TotalCount { get; set; }

}

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool Done { get; set; }
}

public class ListTodos : EndpointWithoutRequest<ListTodosResponse>
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<ListTodos> _logger;

    public ListTodos(TodoDbContext dbContext, ILogger<ListTodos> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override void Configure()
    {
        Get("/todoscompact");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        _logger.LogInformation("Getting all todos");
        var todos = await _dbContext.Todos.ToListAsync(ct);
        // Map to response
        var response = new ListTodosResponse
        {
            Items = todos.Select(t => new TodoItem
            {
                Id = t.Id,
                Title = t.Title,
                Done = t.Done
            }).ToList(),
            TotalCount = todos.Count
        };
        await SendAsync(response);
    }
}