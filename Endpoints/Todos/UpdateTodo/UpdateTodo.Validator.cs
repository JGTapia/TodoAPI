
using FastEndpoints;

namespace TodoApi.Endpoints.Todos.UpdateTodo;

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