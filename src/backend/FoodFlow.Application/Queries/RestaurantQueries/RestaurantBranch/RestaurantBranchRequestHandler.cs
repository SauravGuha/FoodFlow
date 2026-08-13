
using AutoMapper;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.RestaurantQueries.RestaurantBranch;

public class RestaurantBranchRequestHandler : IRequestHandler<RestaurantBranchRequest, Result<IEnumerable<BranchDto>>>
{
    private readonly IBranchRepository branchRepository;
    private readonly IMapper mapper;

    public RestaurantBranchRequestHandler(IBranchRepository branchRepository, IMapper mapper)
    {
        this.branchRepository = branchRepository;
        this.mapper = mapper;
    }
    public async Task<Result<IEnumerable<BranchDto>>> Handle(RestaurantBranchRequest request, CancellationToken cancellationToken)
    {
        var branches = await this.branchRepository.GetAllAsync(condition: e => e.RestaurantId == request.RestaurantId, e => e.CreatedAt, cancellationToken: cancellationToken);

        return Result<IEnumerable<BranchDto>>.SetSuccess(this.mapper.Map<List<BranchDto>>(branches), null);
    }
}