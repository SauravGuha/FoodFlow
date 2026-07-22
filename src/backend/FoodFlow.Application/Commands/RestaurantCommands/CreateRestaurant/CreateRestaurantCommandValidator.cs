
using FluentValidation;
using FoolFlow.Application.Commands.RestaurantCommands.CreateRestaurant;


namespace FoodFlow.Application.Commands.RestaurantCommands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    public CreateRestaurantCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Restaurant name is required.")
            .MaximumLength(100).WithMessage("Restaurant name cannot exceed 100 characters.");

        RuleFor(x => x.Gst)
            .NotEmpty().WithMessage("GST number is required.")
            .Matches(@"^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$").WithMessage("Invalid GST number format.");

        RuleFor(x => x.FNumber)
            .NotEmpty().WithMessage("F number is required.")
            .MaximumLength(50).WithMessage("F number cannot exceed 50 characters.");

        RuleFor(x => x.RestaurantOwner)
            .NotNull()
            .WithMessage("Restaurant owner information is required.");

        RuleFor(x => x.RestaurantOwner.Name)
            .NotEmpty()
            .WithMessage("Restaurant owner name is required.")
            .MaximumLength(200)
            .WithMessage("Restaurant owner name cannot exceed 200 characters.");

        RuleFor(x => x.RestaurantOwner.Email)
            .NotEmpty().WithMessage("Restaurant owner email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.RestaurantOwner.PhoneNumber)
            .NotEmpty().WithMessage("Restaurant owner phone number is required.");
    }
}
