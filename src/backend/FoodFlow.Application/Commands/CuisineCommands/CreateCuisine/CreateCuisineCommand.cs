
using MediatR;
using FoodFlow.Application.Common;

namespace FoodFlow.Application.Commands.CuisineCommands.CreateCuisine;

public class CreateCuisineCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = null!;
    public Guid RestaurantId { get; set; }
}
