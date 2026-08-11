
using FoolFlow.Application.Commands.RestaurantCommands.CreateRestaurant;
using MediatR;

namespace FoodFlow.Application.Commands.RestaurantCommands.UpdateRestaurant;

public class UpdateRestaurantCommand : CreateRestaurantCommand
{
    public Guid Id { get; set; }
}