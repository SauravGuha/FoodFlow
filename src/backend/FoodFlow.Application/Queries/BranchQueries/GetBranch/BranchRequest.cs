using FoodFlow.Application.Common;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.BranchQueries;

public class BranchRequest : IRequest<Result<BranchDto>>
{
    public Guid Id { get; set; }
}
