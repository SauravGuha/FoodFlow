using AutoMapper;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.RestaurantModels; // Make sure this matches your namespace

namespace FoodFlow.Application.Profiles;

public class BranchProfile : Profile
{
    public BranchProfile()
    {
        CreateMap<TimeSlot, TimeSlotDto>();
        CreateMap<TimeSlotDto, TimeSlot>();

        CreateMap<OperatingHours, OperatingHoursDto>()
            .ForMember(dest => dest.Schedule, mo => mo.MapFrom(src =>
                src.Schedule.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.ToList()
                )
            ));
        CreateMap<OperatingHoursDto, OperatingHours>()
        .ForMember(dest => dest.Schedule, mo => mo.MapFrom(src =>
            src.Schedule.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.ToList()
            )
        ));

        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>();

        CreateMap<Branch, BranchDto>()
            .ForMember(dest => dest.Address, mo => mo.MapFrom(src => src.AddressDetails));
        CreateMap<BranchDto, Branch>()
        .ForMember(dest => dest.AddressDetails, mo => mo.MapFrom(src => src.Address)); ;
    }
}