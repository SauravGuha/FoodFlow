

using FoodFlow.Domain.Models.OrderModels;
using Microsoft.Extensions.Configuration;

namespace FoodFlow.Application.Services;

public interface IPaymentFactory
{
    IPaymentGateway CreateGateway(IConfiguration configuration);
}

public interface IPaymentGateway
{
    public string Name { get; set; }

    public Task<string> CreateOrder(Order order);

    public bool VerifyPayment(string gatewayOrderid, string paymentId, string signature);


}