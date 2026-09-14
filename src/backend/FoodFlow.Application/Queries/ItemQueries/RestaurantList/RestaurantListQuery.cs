
using FoodFlow.Application.Common;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.ItemQueries.RestaurantList;

public class RestaurantListQuery : IRequest<Result<IEnumerable<RestaurantListDto>>>
{
}