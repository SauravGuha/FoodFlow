using FluentValidation;

namespace FoodFlow.Application.Queries.RestaurantQueries.FilteredRestaurant;

public class FilteredRestaurantRequestValidator : AbstractValidator<FilteredRestaurantRequest>
{
    public FilteredRestaurantRequestValidator()
    {
        RuleFor(request => request.Name)
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters.");

        RuleFor(request => request.FNumber)
            .MaximumLength(50)
            .WithMessage("FNumber must not exceed 50 characters.");

        RuleFor(request => request.GstNumber)
            .MaximumLength(15)
            .WithMessage("GstNumber must not exceed 15 characters.");

        RuleFor(request => request.Status)
            .MaximumLength(20)
            .WithMessage("Status must not exceed 20 characters.");

        RuleFor(request => request.Take)
            .InclusiveBetween(1, 100)
            .WithMessage("Take must be between 1 and 100.");

        RuleFor(request => request.Skip)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Skip must be greater than or equal to 0.");
    }
}