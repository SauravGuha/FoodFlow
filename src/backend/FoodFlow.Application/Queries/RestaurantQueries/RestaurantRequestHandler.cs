
using AutoMapper;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Queries.RestaurantQueries;

public class RestaurantRequestHandler : IRequestHandler<RestaurantRequest, RestaurantDto>
{
    private readonly IMapper mapper;
    private readonly IRestaurantRepository restaurantRepository;

    public RestaurantRequestHandler(IMapper mapper, IRestaurantRepository restaurantRepository)
    {
        this.mapper = mapper;
        this.restaurantRepository = restaurantRepository;
    }
    public async Task<RestaurantDto> Handle(RestaurantRequest request, CancellationToken cancellationToken)
    {
        var restaurantInfo = await this.restaurantRepository.GetByIdAsync(request.Id, cancellationToken,
        nameof(Restaurant.Branches), nameof(Restaurant.Cuisines));
        if (restaurantInfo == null)
        {
            throw new KeyNotFoundException(request.Id.ToString());
        }
        return this.mapper.Map<RestaurantDto>(restaurantInfo);
    }
}