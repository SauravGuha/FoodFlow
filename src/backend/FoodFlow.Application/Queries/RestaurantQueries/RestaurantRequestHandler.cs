
using AutoMapper;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Queries.RestaurantQueries;

public class RestaurantRequestHandler : IRequestHandler<RestaurantRequest, Result<RestaurantDto>>
{
    private readonly IMapper mapper;
    private readonly IRestaurantRepository restaurantRepository;

    public RestaurantRequestHandler(IMapper mapper, IRestaurantRepository restaurantRepository)
    {
        this.mapper = mapper;
        this.restaurantRepository = restaurantRepository;
    }
    public async Task<Result<RestaurantDto>> Handle(RestaurantRequest request, CancellationToken cancellationToken)
    {
        var restaurantInfo = await this.restaurantRepository.GetByIdAsync(request.Id, cancellationToken);
        if (restaurantInfo == null)
        {
            return Result<RestaurantDto>.SetError($"Restaurant not found with ID {request.Id}.", 404);
        }
        return Result<RestaurantDto>.SetSuccess(this.mapper.Map<RestaurantDto>(restaurantInfo), null);
    }
}