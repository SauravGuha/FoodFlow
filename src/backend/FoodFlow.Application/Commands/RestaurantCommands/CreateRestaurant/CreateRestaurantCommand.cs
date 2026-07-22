

using MediatR;
using FoodFlow.Application.DTOModels;

namespace FoolFlow.Application.Commands.RestaurantCommands.CreateRestaurant;

public class CreateRestaurantCommand : IRequest<Guid>
{
    public string Name { get; set; } = null!;
    public string Gst { get; set; } = null!;
    public string FNumber { get; set; } = null!;
    public string? Description { get; set; }
    public RestaurantOwnerDto RestaurantOwner { get; set; } = null!;
}
