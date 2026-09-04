
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using MediatR;

namespace FoodFlow.Application.Commands.AddStock;

public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, Result<Unit>>
{
    private readonly IFoodFlowContext context;
    private readonly IBranchInventoryRepository branchInventoryRepository;
    private readonly IItemRepository itemRepository;
    private readonly IBranchRepository branchRepository;

    public UpdateStockCommandHandler(IBranchInventoryRepository branchInventoryRepository,
    IItemRepository itemRepository, IBranchRepository branchRepository, IFoodFlowContext foodFlowContext)
    {
        this.context = foodFlowContext;
        this.branchInventoryRepository = branchInventoryRepository;
        this.itemRepository = itemRepository;
        this.branchRepository = branchRepository;
    }
    public async Task<Result<Unit>> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        var item = await itemRepository.GetByIdAsync(request.ItemId, cancellationToken);
        if (item == null)
        {
            return Result<Unit>.SetError($"Item not found", 400);
        }
        var branch = await branchRepository.GetByIdAsync(request.BranchId, cancellationToken);
        if (branch == null)
        {
            return Result<Unit>.SetError($"Branch not found", 400);
        }
        if (branch.RestaurantId != item.RestaurantId)
        {
            return Result<Unit>.SetError($"Item and branch are not in the same restaurant", 400);
        }

        var existingItems = await this.branchInventoryRepository.GetAllAsync(bi => bi.ItemId == request.ItemId && bi.BranchId == request.BranchId,
        bi => bi.CreatedAt, cancellationToken: cancellationToken);
        //check exisitingItems count is equal to one
        if (!existingItems.Any())
        {
            return Result<Unit>.SetError("Item does not exist in the branch", 400);
        }
        var branchItemInventory = existingItems.First();
        if (request.Quantity < 0)
            branchItemInventory.RemoveQuantity(Math.Abs(request.Quantity));
        else
            branchItemInventory.AddQuantity(request.Quantity);

        await this.branchInventoryRepository.UpdateAsync(branchItemInventory, cancellationToken: cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result<Unit>.SetSuccess(Unit.Value, 201);

    }
}