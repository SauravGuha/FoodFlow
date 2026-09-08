
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.BranchQueries.GetBranchLowInventoryItem;

public class GetBranchLowInventoryItemQueryHandler
: IRequestHandler<GetBranchLowInventoryItemQuery, Result<IEnumerable<ItemBranchInventory>>>
{
    private readonly IBranchInventoryRepository branchInventoryRepository;

    public GetBranchLowInventoryItemQueryHandler(IBranchInventoryRepository branchInventoryRepository)
    {
        this.branchInventoryRepository = branchInventoryRepository;
    }

    public async Task<Result<IEnumerable<ItemBranchInventory>>> Handle(GetBranchLowInventoryItemQuery request, CancellationToken cancellationToken)
    {
        var result = await branchInventoryRepository.GetItemBranchInventory(request.BranchId, request.Quantity);

        return Result<IEnumerable<ItemBranchInventory>>.SetSuccess(result, null);
    }
}