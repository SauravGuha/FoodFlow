import { Card, Row, Col, Button } from "react-bootstrap";
import type { CartItem, CartSummary } from "../../../common/types";

export const Order = () => {
  const cartSummaryString = sessionStorage.getItem("cart");
  if (!cartSummaryString) {
    return <>No Cart data found</>;
  }
  const cartSummary = JSON.parse(cartSummaryString) as CartSummary;
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
              <OrderItem key={idx} item={item} />
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
          <Button variant="primary" onClick={() => console.log("Order placed")}>
            Place Order
          </Button>
        </Col>
      </Row>
    </Card>
  );
};

export const OrderItem = ({ item }: { item: CartItem }) => (
  <p>{item.itemName}</p>
);
