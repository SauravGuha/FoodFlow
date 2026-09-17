import { Button, Card, Container } from "react-bootstrap";
import type { CartSummary } from "../../../common/types";
import { useAuth } from "../../../common/auth/AuthContext";
import { useNavigate } from "react-router";

export default function Cart() {
  const navigate = useNavigate();
  const cartSummaryString = sessionStorage.getItem("cart");
  const { isAuthenticated, login } = useAuth();
  if (!cartSummaryString) {
    return <>No Cart data found</>;
  }

  const cartSummary = JSON.parse(cartSummaryString) as CartSummary;

  function handleProceedToOrder() {
    if (!isAuthenticated) {
      login();
    } else {
      navigate("/customer/order");
    }
  }

  return (
    <Container fluid className="py-4">
      {/* Header */}
      <div className="mb-4">
        <h3 className="mb-1">Your Cart</h3>
        <p className="text-muted mb-0">ABC Restaurant · Sealdah Branch</p>
      </div>

      {/* Cart Items */}
      <Card className="border-0 shadow-sm mb-4">
        <Card.Body>
          {/* Item */}
          {cartSummary.cartItems.map((e) => (
            <div className="d-flex justify-content-between align-items-center py-3 border-bottom">
              <div>
                <h6 className="mb-1">{e.itemName}</h6>
                <div className="text-muted small">
                  ₹{e.price} × {e.orderQuantity}
                </div>
              </div>

              <div className="d-flex align-items-center border rounded">
                <Button variant="light" size="sm">
                  −
                </Button>

                <span className="px-3 fw-semibold">{e.orderQuantity}</span>

                <Button variant="light" size="sm">
                  +
                </Button>
              </div>

              <div className="fw-semibold">₹{e.price * e.orderQuantity}</div>
            </div>
          ))}
        </Card.Body>
      </Card>

      {/* Summary */}
      <Card className="border-0 shadow-sm">
        <Card.Body>
          <div className="d-flex justify-content-between mb-2">
            <span>Subtotal</span>
            <strong>₹{cartSummary.cartTotal.toString()}</strong>
          </div>

          <div className="d-flex justify-content-between text-muted small mb-3">
            <span>Items</span>
            <span>{cartSummary.cartItems.length}</span>
          </div>

          <hr />

          <div className="d-flex justify-content-between align-items-center mb-3">
            <strong>Total</strong>
            <h5 className="mb-0">₹{(cartSummary.cartTotal + 10).toString()}</h5>
          </div>

          <Button
            variant="primary"
            className="w-100"
            onClick={() => {
              handleProceedToOrder();
            }}
          >
            Proceed to Order
          </Button>
        </Card.Body>
      </Card>
    </Container>
  );
}
