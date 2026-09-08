
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.BranchQueries.GetBranchInventory;

public class GetBranchItemQueryHandler : IRequestHandler<GetBranchItemQuery, Result<IEnumerable<ItemBranchInventory>>>
{
    private readonly IBranchInventoryRepository branchInventoryRepository;

    public GetBranchItemQueryHandler(IBranchInventoryRepository branchInventoryRepository)
    {
        this.branchInventoryRepository = branchInventoryRepository;
    }

    public async Task<Result<IEnumerable<ItemBranchInventory>>> Handle(GetBranchItemQuery request, CancellationToken cancellationToken)
    {
        var result = await branchInventoryRepository.GetItemBranchInventory(request.BranchId);

        return Result<IEnumerable<ItemBranchInventory>>.SetSuccess(result, null);
    }
}
