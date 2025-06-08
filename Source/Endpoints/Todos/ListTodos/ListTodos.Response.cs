
using TodoApi.Models;

namespace TodoApi.Endpoints.Todos.ListTodos;

public class ListTodosResponse
{
    public List<TodoItem>? Items { get; set; }
    public int TotalCount { get; set; }

}
public class ListTodosModelResponse
{
    public List<Todo>? Items { get; set; }
    public int TotalCount { get; set; }

}

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool Done { get; set; }
}