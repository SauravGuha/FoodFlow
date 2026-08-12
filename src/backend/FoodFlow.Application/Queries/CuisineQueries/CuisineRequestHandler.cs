
using AutoMapper;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Queries.CuisineQueries;

public class CuisineRequestHandler : IRequestHandler<CuisineRequest, Result<IEnumerable<CuisineDto>>>
{
    private readonly IMapper mapper;
    private readonly ICuisineRepository cuisineRepository;
    private readonly IRestaurantRepository restaurantRepository;

    public CuisineRequestHandler(IMapper mapper, ICuisineRepository cuisineRepository, IRestaurantRepository restaurantRepository)
    {
        this.mapper = mapper;
        this.cuisineRepository = cuisineRepository;
        this.restaurantRepository = restaurantRepository;
    }

    public async Task<Result<IEnumerable<CuisineDto>>> Handle(CuisineRequest request, CancellationToken cancellationToken)
    {
        var restaurant = await this.restaurantRepository.GetByIdAsync(request.RestaurantId, cancellationToken);
        if (restaurant == null)
        {
            return Result<IEnumerable<CuisineDto>>.SetError($"Restaurant with id {request.RestaurantId} not found.", 404);
        }

        var cuisines = await this.cuisineRepository.GetAllAsync(
            condition: c => c.RestaurantId == request.RestaurantId,
            orderBy: c => c.CreatedAt,
            cancellationToken: cancellationToken);

        var cuisineDtos = this.mapper.Map<List<CuisineDto>>(cuisines);
        return Result<IEnumerable<CuisineDto>>.SetSuccess(cuisineDtos, null);
    }
}
