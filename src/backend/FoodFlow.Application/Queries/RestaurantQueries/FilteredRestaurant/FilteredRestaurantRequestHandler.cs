using System.Linq.Expressions;
using AutoMapper;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Queries.RestaurantQueries.FilteredRestaurant;

public class FilteredRestaurantRequestHandler : IRequestHandler<FilteredRestaurantRequest, Result<IEnumerable<RestaurantDto>>>
{
    private readonly IRestaurantRepository restaurantRepository;
    private readonly IMapper mapper;

    public FilteredRestaurantRequestHandler(IRestaurantRepository restaurantRepository,
    IMapper mapper)
    {
        this.restaurantRepository = restaurantRepository;
        this.mapper = mapper;
    }

    public async Task<Result<IEnumerable<RestaurantDto>>> Handle(FilteredRestaurantRequest request, CancellationToken cancellationToken)
    {
        var defaultConstant = Expression.Constant(1);
        var condition = Expression.Equal(defaultConstant, defaultConstant);
        var parameter = Expression.Parameter(typeof(Restaurant), "r");

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            var propertyExpression = Expression.Property(parameter, nameof(Restaurant.Name));
            var nameValue = Expression.Constant(request.Name.Trim(' '));
            var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!;
            var containsExpression = Expression.Call(propertyExpression, containsMethod, nameValue);
            condition = Expression.AndAlso(condition, containsExpression);
        }
        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            var statusProp = Expression.Property(parameter, nameof(Restaurant.Status));
            var value = Expression.Constant(request.Status.Trim(' '));
            condition = Expression.AndAlso(condition, Expression.Equal(statusProp, value));
        }
        var whereLambda = Expression.Lambda<Func<Restaurant, bool>>(condition, parameter);

        var orderProperty = Expression.Property(parameter, nameof(Restaurant.Name));
        if (request.SortBy == RestaurantSortField.Status)
            orderProperty = Expression.Property(parameter, nameof(Restaurant.Status));
        var orderBy = Expression.Lambda<Func<Restaurant, string>>(orderProperty, parameter);

        var result = await this.restaurantRepository.GetAllAsync(whereLambda, orderBy, request.Skip, request.Take,
        cancellationToken);
        var total = await this.restaurantRepository.GetQueryCount(whereLambda, orderBy, cancellationToken);
        request.Skip = request.Skip + request.Take;

        var valueDictionary = new Dictionary<string, object>
        {
            {"next", CursorQueryHelper<FilteredRestaurantRequest>.GenerateQueryParams(request)},
            {"total", total}
        };

        return Result<IEnumerable<RestaurantDto>>.SetSuccess(this.mapper.Map<List<RestaurantDto>>(result),
        valueDictionary);
    }
}