using AutoMapper;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.InventoryModels;

namespace FoodFlow.Application.Profiles;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        CreateMap<Item, ItemDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
            .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => src.RestaurantId))
            .ForMember(dest => dest.CuisineId, opt => opt.MapFrom(src => src.CuisineId))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category));
    }
}