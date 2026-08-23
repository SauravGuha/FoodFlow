
using System.Linq.Expressions;
using AutoMapper;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.InventoryModels;
using MediatR;

namespace FoodFlow.Application.Queries.ItemQueries.FilteredItem;

public class FilteredItemRequestHandler : IRequestHandler<FilteredItemRequest, Result<IEnumerable<ItemDto>>>
{
    private readonly IItemRepository itemRepository;
    private readonly IMapper mapper;

    public FilteredItemRequestHandler(IItemRepository itemRepository, IMapper mapper)
    {
        this.itemRepository = itemRepository;
        this.mapper = mapper;
    }

    public async Task<Result<IEnumerable<ItemDto>>> Handle(FilteredItemRequest request, CancellationToken cancellationToken)
    {
        var parameter = Expression.Parameter(typeof(Item), "i");
        var condition = Expression.Equal(Expression.Constant(1), Expression.Constant(1));

        if (request.RestaurantId.HasValue)
        {
            var property = Expression.Property(parameter, nameof(Item.RestaurantId));
            condition = Expression.AndAlso(condition, Expression.Equal(property, Expression.Constant(request.RestaurantId)));
        }
        if (request.CuisineId.HasValue)
        {
            var property = Expression.Property(parameter, nameof(Item.CuisineId));
            condition = Expression.AndAlso(condition, Expression.Equal(property, Expression.Constant(request.CuisineId)));
        }
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            var property = Expression.Property(parameter, nameof(Item.Name));
            var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
            var likeExpression = Expression.Call(property, containsMethod!, Expression.Constant(request.Name.Trim()));
            condition = Expression.AndAlso(condition, likeExpression);
        }
        if (!string.IsNullOrWhiteSpace(request.CategoryName))
        {
            var property = Expression.Property(parameter, nameof(Item.Category));
            var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
            var likeExpression = Expression.Call(property, containsMethod!, Expression.Constant(request.CategoryName.Trim()));
            condition = Expression.AndAlso(condition, likeExpression);
        }
        var lmabda = Expression.Lambda<Func<Item, bool>>(condition, parameter);

        var result = await this.itemRepository.GetAllAsync(lmabda, item => item.CreatedAt, cancellationToken: cancellationToken);

        return Result<IEnumerable<ItemDto>>.SetSuccess(mapper.Map<IEnumerable<ItemDto>>(result), null);
    }
}