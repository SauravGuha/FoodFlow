
using FluentValidation;

namespace FoodFlow.Application.Commands.CuisineCommands.CreateCuisine;

public class CreateCuisineCommandValidator : AbstractValidator<CreateCuisineCommand>
{
    public CreateCuisineCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Cuisine name is required.")
            .MaximumLength(100).WithMessage("Cuisine name cannot exceed 100 characters.");

        RuleFor(x => x.RestaurantId)
            .NotEmpty().WithMessage("Restaurant id is required.");
    }
}
