
using FoodFlow.Application.Common;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.RestaurantQueries;

public class RestaurantRequest : IRequest<Result<RestaurantDto>>
{
    public Guid Id { get; set; }
}