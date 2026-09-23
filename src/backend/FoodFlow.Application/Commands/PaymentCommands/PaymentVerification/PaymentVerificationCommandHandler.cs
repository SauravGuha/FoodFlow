
using FoodFlow.Application.Common;
using FoodFlow.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace FoodFlow.Application.Commands.PaymentCommands.PaymentVerfication;

public class PaymentVerificationCommandHandler : IRequestHandler<PaymentVerificationCommand, Result<Unit>>
{
    private IPaymentGateway paymentGateway;

    public PaymentVerificationCommandHandler(IPaymentFactory paymentFactory, IConfiguration configuration)
    {
        this.paymentGateway = paymentFactory.CreateGateway(configuration);
    }
    public Task<Result<Unit>> Handle(PaymentVerificationCommand request, CancellationToken cancellationToken)
    {
        var result = this.paymentGateway.VerifyPayment(request.RazorpayOrderId, request.RazorpayPaymentId, request.RazorpaySignature);
        return Task.FromResult(result ? Result<Unit>.SetSuccess(default(Unit), null) : Result<Unit>.SetError("Payment signature mismatch", 422));
    }
}