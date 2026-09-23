
using FoodFlow.Application.Common;
using MediatR;

namespace FoodFlow.Application.Commands.PaymentCommands.PaymentVerfication;

public class PaymentVerificationCommand : IRequest<Result<Unit>>
{
    public string RazorpayPaymentId { get; set; }


    public string RazorpayOrderId { get; set; }

    public string RazorpaySignature { get; set; }
}