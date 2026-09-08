using FoodFlow.Application.Common;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.ItemQueries;

public class GetItemByIdQuery : IRequest<Result<ItemDto>>
{
    public Guid Id { get; set; }
}