
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.InventoryModels;
using MediatR;

namespace FoodFlow.Application.Commands.InventoryCommands.AddBranchInventory;

public class AddBranchInventoryCommandHandler : IRequestHandler<AddBranchInventoryCommand, Result<Guid>>
{
    private readonly IBranchInventoryRepository branchInventoryRepository;
    private readonly IItemRepository itemRepository;
    private readonly IBranchRepository branchRepository;
    private readonly IFoodFlowContext foodFlowContext;

    public AddBranchInventoryCommandHandler(IBranchInventoryRepository branchInventoryRepository,
    IItemRepository itemRepository, IBranchRepository branchRepository, IFoodFlowContext foodFlowContext)
    {
        this.branchInventoryRepository = branchInventoryRepository;
        this.itemRepository = itemRepository;
        this.branchRepository = branchRepository;
        this.foodFlowContext = foodFlowContext;
    }
    public async Task<Result<Guid>> Handle(AddBranchInventoryCommand request, CancellationToken cancellationToken)
    {
        var item = await itemRepository.GetByIdAsync(request.ItemId, cancellationToken);
        if (item == null)
        {
            return Result<Guid>.SetError($"Item not found", 400);
        }
        var branch = await branchRepository.GetByIdAsync(request.BranchId, cancellationToken);
        if (branch == null)
        {
            return Result<Guid>.SetError($"Branch not found", 400);
        }
        if (branch.RestaurantId != item.RestaurantId)
        {
            return Result<Guid>.SetError($"Item and branch are not in the same restaurant", 400);
        }
        if (request.Quantity < 0)
        {
            return Result<Guid>.SetError($"Quantity cannot be negative", 400);
        }
        var existingInventory = await branchInventoryRepository.GetAllAsync(condition: (bi) => bi.BranchId == request.BranchId && bi.ItemId == request.ItemId,
        orderBy: e => e.CreatedAt,
        cancellationToken: cancellationToken);
        if (existingInventory.Any())
        {
            return Result<Guid>.SetError($"Item already exists in the branch", 409);
        }

        var itemInventory = new BranchInventory(request.ItemId, request.BranchId);
        await branchInventoryRepository.AddAsync(itemInventory, cancellationToken);
        await foodFlowContext.SaveChangesAsync(cancellationToken);
        return Result<Guid>.SetSuccess(itemInventory.Id, null);
    }
}