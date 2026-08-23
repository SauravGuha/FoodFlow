
using FoodFlow.Application.Common;
using MediatR;

namespace FoodFlow.Application.Commands.ItemCommands.CreateItem;

public class CreateItemCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string Sku { get; set; } = default!;

    public Guid RestaurantId { get; set; }

    public Guid CuisineId { get; set; }

    public string Category { get; set; } = default!;
}