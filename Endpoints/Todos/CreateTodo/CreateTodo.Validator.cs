
using FastEndpoints;

namespace TodoApi.Endpoints.Todos.CreateTodo;

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