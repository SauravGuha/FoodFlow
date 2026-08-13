
using FoodFlow.Application.Common;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Commands.RestaurantCommands.UpdateRestaurantStatus;

public class UpdateRestaurantStatusCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public RestaurantStatus Status { get; set; }
}
