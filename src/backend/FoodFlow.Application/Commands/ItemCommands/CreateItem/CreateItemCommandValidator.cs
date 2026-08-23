
using FluentValidation;

namespace FoodFlow.Application.Commands.ItemCommands.CreateItem;

public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
        RuleFor(e => e.Name)
        .NotNull()
        .WithMessage("Cannot be empty");
    }
}