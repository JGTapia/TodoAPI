using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Endpoints.Todos.ListTodos;

public class ListTodosEndpoint : EndpointWithoutRequest<ListTodosModelResponse>
{
    private readonly TodoDbContext _dbContext;
    private readonly ILogger<ListTodosEndpoint> _logger;

    public ListTodosEndpoint(TodoDbContext dbContext, ILogger<ListTodosEndpoint> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override void Configure()
    {
        Get("/todos");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        _logger.LogInformation("[GET /todos]: Getting all todos");
        var todos = await _dbContext.Todos.ToListAsync(ct);
        // Map to response
        var response = new ListTodosModelResponse
        {
            Items = todos.Select(t => new Todo { Id = t.Id, Title = t.Title, Done = t.Done }).ToList(),
            TotalCount = todos.Count
        };

        _logger.LogInformation("Returning " + response.TotalCount.ToString() + " Todos");
        await SendAsync(response);
    }
}