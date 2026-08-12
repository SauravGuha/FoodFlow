
using FoodFlow.Application.Common;
using MediatR;

namespace FoodFlow.Application.Commands.CuisineCommands.DeleteCuisine;

public class DeleteCuisineCommand : IRequest<Result<bool>>
{
    public Guid CuisineId { get; set; }
    public Guid RestaurantId { get; set; }
}
