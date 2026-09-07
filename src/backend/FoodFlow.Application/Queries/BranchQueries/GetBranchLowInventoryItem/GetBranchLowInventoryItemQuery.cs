
using FoodFlow.Application.Common;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.BranchQueries.GetBranchLowInventoryItem;

public class GetBranchLowInventoryItemQuery : IRequest<Result<IEnumerable<ItemBranchInventory>>>
{
    public Guid BranchId { get; set; }

    public int Quantity { get; set; }

}