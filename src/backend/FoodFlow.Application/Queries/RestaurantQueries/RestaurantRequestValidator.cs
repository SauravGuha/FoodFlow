
using FluentValidation;

namespace FoodFlow.Application.Queries.RestaurantQueries;

public class RestaurantRequestValidator : AbstractValidator<RestaurantRequest>
{
    public RestaurantRequestValidator()
    {
        this.RuleFor(e => e.Id)
        .NotEmpty()
        .WithMessage("Id is required");
    }
}