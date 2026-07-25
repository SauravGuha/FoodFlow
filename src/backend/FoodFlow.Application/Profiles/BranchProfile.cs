using AutoMapper;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.RestaurantModels; // Make sure this matches your namespace

namespace FoodFlow.Application.Profiles;

public class BranchProfile : Profile
{
    public BranchProfile()
    {
        CreateMap<Branch, BranchDto>();
        CreateMap<BranchDto, Branch>();
    }
}