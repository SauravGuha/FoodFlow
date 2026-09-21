import { Button, Card, Container } from "react-bootstrap";
import { useNavigate, useParams } from "react-router";
import LoaderContext from "../../../common/utilities/appContext";
import { useContext, useEffect, useState } from "react";
import { getOrderDetails } from "../../../common/utilities/apiHelper";
import type { Order } from "../../../common/types";

export default function OrderConfirmation() {
  const navigate = useNavigate();
  const { orderId } = useParams();
  const loaderContext = useContext(LoaderContext);
  const { setLoading, loaderStatus } = loaderContext!;
  const [order, setOrder] = useState<Order | undefined>(undefined);

  useEffect(() => {
    if (orderId) {
      setLoading(true);
      getOrderDetails(orderId)
        .then((response) => {
          setOrder(response.data);
        })
        .finally(() => setLoading(false));
    }
  }, []);

  if (loaderStatus) return <>Loading...</>;

  return (
    <Container className="py-5" style={{ maxWidth: "700px" }}>
      {/* Success */}
      <div className="text-center mb-4">
        <div
          className="rounded-circle bg-success bg-opacity-10 text-success d-inline-flex align-items-center justify-content-center mb-3"
          style={{ width: "72px", height: "72px", fontSize: "36px" }}
        >
          ✓
        </div>

        <h2 className="fw-bold mb-2">Order placed successfully!</h2>

        <p className="text-muted mb-0">
          Thank you for your order. Your order has been confirmed.
        </p>
      </div>

      {/* Order details */}
      <Card className="border-0 shadow-sm mb-4">
        <Card.Body className="p-4">
          <div className="d-flex justify-content-between align-items-start mb-4">
            <div>
              <div className="text-muted small">Order</div>
              <h5 className="mb-0">#{orderId}</h5>
            </div>

            <span className="badge bg-success">{order?.status}</span>
          </div>

          <div className="mb-4">
            <div className="fw-semibold"></div>
            <div className="text-muted small">{order?.branch.name}</div>
          </div>

          {/* Items */}
          {order?.orderItemDtos.map((oi) => (
            <div
              className="border-top border-bottom"
              key={oi.branchInventoryId}
            >
              <div className="d-flex justify-content-between py-3">
                <div>
                  <div className="fw-semibold">{oi.itemName}</div>
                  <div className="text-muted small">
                    ₹{oi.quantity} × {oi.unitPrice}
                  </div>
                </div>

                <span className="fw-semibold">₹{oi.unitPrice}</span>
              </div>
            </div>
          ))}

          {/* Total */}
          <div className="d-flex justify-content-between align-items-center pt-4">
            <span className="fw-semibold">Total</span>
            <span className="fs-5 fw-bold">₹ To calculate</span>
          </div>
        </Card.Body>
      </Card>

      {/* Actions */}
      <div className="d-grid gap-2">
        <Button variant="primary" onClick={() => navigate("/customer/orders")}>
          View My Orders
        </Button>

        <Button
          variant="outline-secondary"
          onClick={() => navigate("/customer/restaurants")}
        >
          Continue Browsing
        </Button>
      </div>
    </Container>
  );
}
