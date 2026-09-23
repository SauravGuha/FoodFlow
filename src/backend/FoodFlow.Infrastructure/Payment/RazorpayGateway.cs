
using System.Security.Cryptography;
using System.Text;
using FoodFlow.Application.Services;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;

namespace FoodFlow.Infrastructure.Payment;

public class RazorpayPaymentFactory : IPaymentFactory
{
    public IPaymentGateway CreateGateway(IConfiguration configuration)
    {
        var apiKey = configuration.GetSection("Razorpay:apiKey").Value;
        var apiSecret = configuration.GetSection("Razorpay:apiSecret").Value;

        if (apiKey == null || apiSecret == null)
            throw new ArgumentException("Missing Razorpay API key or secret");

        return new RazorpayPaymentGateway(apiKey, apiSecret);
    }
}

public class RazorpayPaymentGateway : IPaymentGateway
{
    public RazorpayPaymentGateway(string apiKey, string apiSecret)
    {
        this.ApiKey = apiKey;
        this.ApiSecret = apiSecret;
    }

    public string Name { get; set; } = "RazorPay";

    public string ApiKey { get; set; }

    public string ApiSecret { get; set; }

    public Task<string> CreateOrder(FoodFlow.Domain.Models.OrderModels.Order order)
    {
        RazorpayClient client = new RazorpayClient(this.ApiKey, this.ApiSecret);

        Dictionary<string, object> options = new Dictionary<string, object>();
        options.Add("amount", order.CalculateTotalPrice() * 100);
        options.Add("currency", "INR");
        options.Add("receipt", order.Id.ToString());
        var razorpayOrder = client.Order.Create(options);

        return Task.FromResult(razorpayOrder["id"].ToString());
    }

    public bool VerifyPayment(string gatewayOrderid, string paymentId, string signature)
    {
        using (var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(ApiSecret)))
        {
            byte[] hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes($"{gatewayOrderid}|{paymentId}"));
            var actualSignature = Convert.ToBase64String(hash);
            return actualSignature == signature;
        }
    }
}