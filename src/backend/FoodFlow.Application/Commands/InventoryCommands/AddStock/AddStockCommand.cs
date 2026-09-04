
using FoodFlow.Application.Common;
using MediatR;

namespace FoodFlow.Application.Commands.AddStock;

public class AddStockCommand : IRequest<Result<Unit>>
{
    public Guid BranchId { get; set; }

    public Guid ItemId { get; set; }

    public int Quantity { get; set; }

}