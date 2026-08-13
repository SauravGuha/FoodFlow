using AutoMapper;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.RestaurantModels; // Make sure this matches your namespace

namespace FoodFlow.Application.Profiles;

public class BranchProfile : Profile
{
    public BranchProfile()
    {
        CreateMap<TimeSlot, TimeSlotDto>();
        CreateMap<OperatingHours, OperatingHoursDto>()
            .ForMember(dest => dest.Schedule, mo => mo.MapFrom(src =>
                src.Schedule.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.ToList()
                )
            ));
        CreateMap<Address, AddressDto>()
            .ConstructUsing(src => new AddressDto
            {
                Street = src.Street,
                City = src.City,
                State = src.State,
                ZipCode = src.ZipCode,
                Country = src.Country
            });
        CreateMap<Branch, BranchDto>()
            .ForMember(dest => dest.Address, mo => mo.MapFrom(src => src.AddressDetails));
        CreateMap<BranchDto, Branch>();
    }
}