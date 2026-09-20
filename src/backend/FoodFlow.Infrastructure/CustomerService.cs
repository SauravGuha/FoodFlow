
using System.Security.Claims;
using FoodFlow.Application.DTOModels;
using FoodFlow.Application.Services;
using Microsoft.AspNetCore.Http;

namespace FoodFlow.Infrastructure;

public class CustomerService : ICustomerService
{
    private IHttpContextAccessor accessor;

    public CustomerService(IHttpContextAccessor accessor)
    {
        this.accessor = accessor;
    }
    public Task<CustomerDto> GetCustomerDetailAsync()
    {
        var result = new CustomerDto();
        result.Email = this.accessor.HttpContext?.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email)?.Value ?? "";

        return Task.FromResult(result);
    }
}