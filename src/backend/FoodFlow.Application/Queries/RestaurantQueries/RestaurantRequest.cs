
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.RestaurantQueries;

public class RestaurantRequest : IRequest<RestaurantDto>
{
    public Guid Id { get; set; }
}