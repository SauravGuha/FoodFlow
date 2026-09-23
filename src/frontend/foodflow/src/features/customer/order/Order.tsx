import { Card, Row, Col, Button, ProgressBar, Spinner } from "react-bootstrap";
import type {
  Address,
  CartItem,
  CartSummary,
  CreateOrderRequest,
  OrderItem,
} from "../../../common/types";
import { placeOrder } from "../../../common/utilities/apiHelper";
import { useState } from "react";
import { useNavigate } from "react-router";

export const Order = () => {
  const navigate = useNavigate();
  const [submitting, setSubmitting] = useState<boolean>(false);
  const cartSummaryString = sessionStorage.getItem("cart");
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
    const orderId = await placeOrder(orderRequest);

    setSubmitting(false);
    sessionStorage.removeItem("cart");

    navigate(`/customer/order-confirmation/${orderId}`);
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
