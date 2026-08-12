
using FoodFlow.Application.Common;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.CuisineQueries;

public class CuisineRequest : IRequest<Result<IEnumerable<CuisineDto>>>
{
    public Guid RestaurantId { get; set; }
}
