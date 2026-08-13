
using FoodFlow.Application.Common;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.RestaurantQueries.RestaurantBranch;

public class RestaurantBranchRequest : IRequest<Result<IEnumerable<BranchDto>>>
{
    public Guid RestaurantId { get; set; }
}