
using FoodFlow.Application.Common;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.ItemQueries.FilteredItem;

public class FilteredItemRequest : IRequest<Result<IEnumerable<ItemDto>>>
{
    public Guid? RestaurantId { get; set; }
    public Guid? CuisineId { get; set; }

    public string? CategoryName { get; set; } = default!;

    public string? Name { get; set; } = default!;

    public int Take { get; set; } = 10;

    public int Skip { get; set; } = 0;
}