using AutoMapper;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.BranchQueries;

public class BranchRequestHandler : IRequestHandler<BranchRequest, Result<BranchDto>>
{
    private readonly IMapper mapper;
    private readonly IBranchRepository branchRepository;

    public BranchRequestHandler(IMapper mapper, IBranchRepository branchRepository)
    {
        this.mapper = mapper;
        this.branchRepository = branchRepository;
    }

    public async Task<Result<BranchDto>> Handle(BranchRequest request, CancellationToken cancellationToken)
    {
        var branchInfo = await this.branchRepository.GetByIdAsync(request.Id, cancellationToken);
        if (branchInfo == null)
        {
            return Result<BranchDto>.SetError($"Branch not found with ID {request.Id}.", 404);
        }
        return Result<BranchDto>.SetSuccess(this.mapper.Map<BranchDto>(branchInfo), null);
    }
}
