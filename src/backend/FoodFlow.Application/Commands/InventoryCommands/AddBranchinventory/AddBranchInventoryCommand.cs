
using FoodFlow.Application.Common;
using MediatR;

namespace FoodFlow.Application.Commands.InventoryCommands.AddBranchInventory;

public class AddBranchInventoryCommand : IRequest<Result<Guid>>
{
    public Guid ItemId { get; set; }

    public Guid BranchId { get; set; }

    public int Quantity { get; set; }
}