
using AutoMapper;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.RestaurantModels;

namespace FoodFlow.Application.Profiles;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantDto>()
        .ForMember(dest => dest.Status, mo => mo.MapFrom(r => r.Status.ToString()));
    }
}