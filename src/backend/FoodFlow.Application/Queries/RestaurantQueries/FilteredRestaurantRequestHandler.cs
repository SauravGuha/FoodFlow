using System.Linq.Expressions;
using AutoMapper;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Queries.RestaurantQueries;

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
            condition = Expression.AndAlso(condition, Expression.Equal(propertyExpression, nameValue));
        }
        var whereLambda = Expression.Lambda<Func<Restaurant, bool>>(condition, parameter);

        var orderProperty = Expression.Property(parameter, nameof(Restaurant.Name));
        var orderBy = Expression.Lambda<Func<Restaurant, string>>(orderProperty, parameter);

        var result = await this.restaurantRepository.GetAllAsync(whereLambda, orderBy, cancellationToken);
        var total = await this.restaurantRepository.GetQueryCount(whereLambda, orderBy, cancellationToken);

        return Result<IEnumerable<RestaurantDto>>.SetSuccess(this.mapper.Map<List<RestaurantDto>>(result),
        total);
    }
}