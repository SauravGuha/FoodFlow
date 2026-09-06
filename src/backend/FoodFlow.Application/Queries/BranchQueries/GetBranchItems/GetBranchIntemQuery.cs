
using FoodFlow.Application.Common;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.BranchQueries.GetBranchInventory;

public class GetBranchItemQuery : IRequest<Result<IEnumerable<ItemBranchInventory>>>
{
    public Guid BranchId { get; set; }

}