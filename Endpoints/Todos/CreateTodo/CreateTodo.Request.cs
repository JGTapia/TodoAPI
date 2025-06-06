
namespace TodoApi.Endpoints.Todos.CreateTodo;

public class CreateTodoRequest
{
    public string Title { get; set; } = string.Empty;
    public bool Done { get; set; }
}