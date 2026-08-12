
using AutoMapper;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.RestaurantModels;

namespace FoodFlow.Application.Profiles;

public class CuisineProfile : Profile
{
    public CuisineProfile()
    {
        CreateMap<Cuisine, CuisineDto>();
    }
}
