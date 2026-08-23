
using AutoMapper;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.ItemQueries;

public class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery, Result<ItemDto>>
{
    private readonly IItemRepository _repo;
    private readonly IMapper _mapper;


    public GetItemByIdHandler(IItemRepository repo, IMapper mapper)
    {
        this._repo = repo;
        this._mapper = mapper;
    }

    public async Task<Result<ItemDto>> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (item == null)
            return Result<ItemDto>.SetError("Item not found", 404);

        var itemDto = this._mapper.Map<ItemDto>(item);
        return Result<ItemDto>.SetSuccess(itemDto, null);
    }
}