
using FoodFlow.Application.Common;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.OrderQueries;

public class OrderRequest : IRequest<Result<OrderDto>>
{
    public Guid Id { get; set; }
}