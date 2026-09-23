
using AutoMapper;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.OrderModels;
using MediatR;

namespace FoodFlow.Application.Queries.OrderQueries.OrderById;

public class OrderRequestHandler : IRequestHandler<OrderRequest, Result<OrderDto>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public OrderRequestHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }
    public async Task<Result<OrderDto>> Handle(OrderRequest request, CancellationToken cancellationToken)
    {
        var result = await this.orderRepository.GetByIdAsync(request.Id, cancellationToken, nameof(Order.Customer), nameof(Order.Branch));
        return Result<OrderDto>.SetSuccess(mapper.Map<OrderDto>(result), null);
    }
}