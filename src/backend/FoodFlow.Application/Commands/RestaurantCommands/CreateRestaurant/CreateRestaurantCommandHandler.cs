
using AutoMapper;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.RestaurantModels;
using FoolFlow.Application.Commands.RestaurantCommands.CreateRestaurant;
using MediatR;

namespace FoodFlow.Application.Commands.RestaurantCommands.CreateRestaurant;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, Result<Guid>>
{
    private readonly IRestaurantRepository restaurantRepository;
    private readonly IFoodFlowContext foodFlowContext;
    private readonly IMapper mapper;

    public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IFoodFlowContext foodFlowContext, IMapper mapper)
    {
        this.restaurantRepository = restaurantRepository;
        this.foodFlowContext = foodFlowContext;
        this.mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        if (await this.restaurantRepository.GetByGstNumberAsync(request.Gst, cancellationToken) != null)
        {
            return Result<Guid>.SetError($"A restaurant with GST number {request.Gst} already exists.", 409);
        }

        var restaurantOwner = this.mapper.Map<RestaurantOwner>(request.RestaurantOwner);
        var restaurant = new Restaurant(request.Name, request.Gst, request.FNumber, restaurantOwner, description: request.Description);

        //database insertion logic
        await this.restaurantRepository.AddAsync(restaurant, cancellationToken);
        await this.foodFlowContext.SaveChangesAsync(cancellationToken);


        return Result<Guid>.SetSuccess(restaurant.Id, null, 201);
    }
}