using System.Linq.Expressions;
using AutoMapper;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Queries.RestaurantQueries;

public class FilteredRestaurantRequestHandler : IRequestHandler<FilteredRestaurantRequest, IEnumerable<RestaurantDto>>
{
    private readonly IRestaurantRepository restaurantRepository;
    private readonly IMapper mapper;

    public FilteredRestaurantRequestHandler(IRestaurantRepository restaurantRepository,
    IMapper mapper)
    {
        this.restaurantRepository = restaurantRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<RestaurantDto>> Handle(FilteredRestaurantRequest request, CancellationToken cancellationToken)
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

        var restaurants = await this.restaurantRepository.GetAllAsync(whereLambda, orderBy, cancellationToken);

        return this.mapper.Map<List<RestaurantDto>>(restaurants); // or map to DTO if needed
    }
}