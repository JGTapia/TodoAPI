
namespace TodoApi.Endpoints.Todos.UpdateTodo;

public class UpdateTodoRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool Done { get; set; }
}