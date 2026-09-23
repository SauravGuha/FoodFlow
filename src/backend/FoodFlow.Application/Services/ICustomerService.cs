
using FoodFlow.Application.DTOModels;

namespace FoodFlow.Application.Services;


public interface ICustomerService
{
    Task<CustomerDto> GetCustomerDetailAsync();
}