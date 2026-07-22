
using AutoMapper;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.RestaurantModels;

namespace FoodFlow.Application.Profiles;

public class RestaurantOwnerProfile : Profile
{
    public RestaurantOwnerProfile()
    {
        CreateMap<RestaurantOwner, RestaurantOwnerDto>()
        .ConvertUsing(owner => new RestaurantOwnerDto
        {
            Name = owner.Name,
            Email = owner.Email,
            PhoneNumber = $"{owner.PhoneNumber.CountryCode}-{owner.PhoneNumber.Number}"
        });

        CreateMap<RestaurantOwnerDto, RestaurantOwner>()
        .ConvertUsing(dto => new RestaurantOwner(
            dto.Name,
            dto.Email,
            new PhoneNumber(dto.PhoneNumber.Split('-')[0], dto.PhoneNumber.Split('-')[1])
        ));
    }
}