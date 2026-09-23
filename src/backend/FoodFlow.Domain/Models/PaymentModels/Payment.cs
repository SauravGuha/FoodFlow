
using System.Net.Http.Headers;
using FoodFlow.Domain.Models.OrderModels;

namespace FoodFlow.Domain.Models.PaymentModels;

public enum PaymentStatus
{
    none,
    //The customer completes payment and funds are held
    authorized,
    //The payment is settled and funds are released for payout.
    captured,
    failed,
    //A chargeback or dispute has been raised.
    disputed,
    //All payments against an order are complete.
    paid,
    refundStarted,
    //A refund could not be processed.
    refundFailed
}

public class Payment : BaseModel
{

    public Guid OrderId { get; private set; }

    public Order Order { get; set; }

    public string ExternalOrderId { get; private set; }

    public string? ExternalPaymentId { get; private set; }

    public decimal OrderAmount { get; private set; }

    public PaymentStatus Status { get; private set; }

    public Payment(Guid orderId, string externalOrderId, decimal orderAmount)
    {
        this.OrderId = orderId;
        this.ExternalOrderId = externalOrderId;
        this.OrderAmount = orderAmount;
        Status = PaymentStatus.none;
    }

    public void UpdateExternalPaymentId(String value)
    {
        this.ExternalPaymentId = value;
    }

    public void UpdatePaymentStatus(PaymentStatus paymentStatus)
    {
        this.Status = paymentStatus;
    }
}