import { Card, Row, Col, Button, Spinner } from "react-bootstrap";
import type {
  Address,
  CartItem,
  CartSummary,
  CreateOrderRequest,
  OrderItem,
} from "../../../common/types";
import { placeOrder } from "../../../common/utilities/apiHelper";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";

declare global {
  interface Window {
    Razorpay: any;
  }
}

export const Order = () => {
  const navigate = useNavigate();
  const [submitting, setSubmitting] = useState<boolean>(false);
  const cartSummaryString = sessionStorage.getItem("cart");
  useEffect(() => {
    const script = document.createElement("script");
    script.src = "https://checkout.razorpay.com/v1/checkout.js";
    script.async = true;
    document.body.appendChild(script);

    return () => {
      document.body.removeChild(script);
    };
  }, []);
  if (!cartSummaryString) {
    return <>No Cart data found</>;
  }

  const cartSummary = JSON.parse(cartSummaryString) as CartSummary;
  async function handlePlaceOrder() {
    setSubmitting(true);
    const orderRequest: CreateOrderRequest = {
      branchId: cartSummary.branchId,
      status: "Pending",
      orderItems: cartSummary.cartItems.map(
        (e) =>
          ({
            branchInventoryId: e.inventoryId,
            discountPercent: 0,
            itemName: e.itemName,
            quantity: e.orderQuantity,
            sku: e.sku,
            unitPrice: e.price,
            taxPercent: 0,
          }) as OrderItem,
      ),
      deliveryAddress: {} as Address,
      billingAddress: {} as Address,
    };
    const orderDetails = await placeOrder(orderRequest);
    var options = {
      key: "rzp_test_TfMgxfjPgD3rcT",
      amount: orderDetails.orderTotal,
      currency: "INR",
      name: "FoodFlow",
      description:
        "Payment for your order-confirmation/${orderDetails.id}`);er",
      image: "https://example.com/your_logo.png",
      order_id: orderDetails.paymentGateWayId,
      handler: function (response: any) {
        // Send ALL THREE fields to your server for verification (Step 3)
        fetch("/payment/verify", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            razorpayPaymentId: response.razorpay_payment_id,
            razorpayOrderId: response.razorpay_order_id,
            razorpaySignature: response.razorpay_signature,
          }),
        }).finally(() => {
          setSubmitting(false);
          sessionStorage.removeItem("cart");
          navigate(`/customer/order-confirmation/${orderDetails.id}`);
        });
      },
      prefill: {
        name: orderDetails.customer.name,
        email: orderDetails.customer.email,
        contact: "",
      },
      notes: { address: "Your Office" },
      theme: { color: "#3399cc" },
      modal: {
        confirm_close: true,
        escape: false,
        backdropclose: false,
        animation: true,
      },
      retry: { enabled: true, max_count: 4 },
    };

    var rzp1 = new window.Razorpay(options);
    rzp1.on("payment.failed", function (response: any) {
      console.error("Payment failed:", {
        code: response.error.code,
        description: response.error.description,
        source: response.error.source,
        step: response.error.step,
        reason: response.error.reason,
        order_id: response.error.metadata.order_id,
        payment_id: response.error.metadata.payment_id,
      });
      // Show the error to the customer and offer a retry.
    });
    rzp1.open();
  }

  return (
    <Card className="order-card">
      <Row>
        <Col xs={12} sm={6}>
          <h3>Place Your Order</h3>
          <p>ABC Restaurant · Sealdah Branch</p>
        </Col>

        <Col xs={12} sm={6}>
          <div className="order-items">
            {cartSummary.cartItems.map((item, idx) => (
              <Items key={idx} item={item} />
            ))}
          </div>
        </Col>

        <Col xs={12} sm={6}>
          <div className="order-summary">
            <p>Subtotal: ₹{cartSummary.cartTotal}</p>
            {/* <p>Delivery / Service Fee: ₹{order.fee}</p> */}
            <p>Total: ₹{cartSummary.cartTotal}</p>
          </div>
        </Col>

        <Col xs={12} sm={6}>
          <Button variant="primary" onClick={() => handlePlaceOrder()}>
            {submitting ? <Spinner size="sm" /> : <></>}
            Place Order
          </Button>
        </Col>
      </Row>
    </Card>
  );
};

export const Items = ({ item }: { item: CartItem }) => <p>{item.itemName}</p>;
