
namespace TodoApi.Endpoints.Todos.GetTodo;

public class GetTodoResponse

{
    public string Title { get; set; } = string.Empty;
    public bool Done { get; set; }
}