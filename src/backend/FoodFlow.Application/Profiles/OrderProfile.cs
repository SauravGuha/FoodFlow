
using AutoMapper;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.CustomerModels;
using FoodFlow.Domain.Models.OrderModels;

namespace FoodFlow.Application.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>()
        .ForMember(o => o.Status, (mo) =>
        {
            mo.MapFrom(o => o.Status.ToString());
        });
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<Customer, CustomerDto>();
    }
}