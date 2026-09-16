import { useContext, useEffect, useState } from "react";
import {
  Badge,
  Button,
  Card,
  Col,
  Container,
  Form,
  InputGroup,
  Row,
} from "react-bootstrap";
import type { BranchInventoryItem } from "../../../common/types";
import { useParams } from "react-router";
import { getBranchInventories } from "../../../common/utilities/apiHelper";
import LoaderContext from "../../../common/utilities/appContext";

export default function BranchMenu() {
  const { branchid } = useParams();
  const [branchInventories, setBranchInventories] = useState<
    BranchInventoryItem[]
  >([]);
  const loaderContext = useContext(LoaderContext);
  const { setLoading, loaderStatus } = loaderContext!;

  useEffect(() => {
    setLoading(true);
    getBranchInventories(branchid)
      .then((response) => {
        setBranchInventories(response.data);
      })
      .finally(() => {
        setLoading(false);
      });
  }, [branchid]);

  if (loaderStatus) return <>Loading...</>;

  return (
    <Container fluid className="py-3">
      {/* Restaurant / Branch Header */}
      <Card className="border-0 shadow-sm mb-3">
        <Card.Body>
          <div className="d-flex justify-content-between align-items-start">
            <div>
              <h3 className="mb-1">Restaurant Name</h3>
              <div className="text-muted">Branch Name · Kolkata</div>

              <div className="mt-2">
                <Badge bg="success" className="me-2">
                  Open
                </Badge>
                <small className="text-muted">Closes at 10:30 PM</small>
              </div>
            </div>

            <div className="text-end">
              <div className="text-muted small">Your order</div>
              <strong>0 items</strong>
            </div>
          </div>
        </Card.Body>
      </Card>

      {/* Search */}
      <InputGroup className="mb-3">
        <InputGroup.Text>🔍</InputGroup.Text>
        <Form.Control placeholder="Search dishes..." />
      </InputGroup>

      {/* Category Navigation */}
      <div
        className="d-flex gap-2 mb-4 overflow-auto pb-2"
        style={{ whiteSpace: "nowrap" }}
      >
        <Button variant="dark" size="sm">
          All
        </Button>

        <Button variant="outline-secondary" size="sm">
          Starters
        </Button>

        <Button variant="outline-secondary" size="sm">
          Main Course
        </Button>

        <Button variant="outline-secondary" size="sm">
          Pizza
        </Button>

        <Button variant="outline-secondary" size="sm">
          Burgers
        </Button>

        <Button variant="outline-secondary" size="sm">
          Desserts
        </Button>
      </div>

      {/* Menu */}
      <h5 className="mb-3">Menu</h5>

      <Row xs={1} md={2} lg={3} className="g-3">
        {/* Menu Item */}
        <Col>
          <Card className="h-100 border-0 shadow-sm">
            <Card.Body>
              <div className="d-flex justify-content-between">
                <div>
                  <div className="mb-1">
                    <span
                      className="d-inline-block border border-success rounded-circle me-2"
                      style={{
                        width: 12,
                        height: 12,
                      }}
                    />

                    <strong>Margherita Pizza</strong>
                  </div>

                  <div className="fw-semibold mb-2">₹299</div>
                </div>

                <Badge bg="light" text="dark">
                  Pizza
                </Badge>
              </div>

              <p className="text-muted small mb-3">
                Classic pizza with tomato sauce, mozzarella and fresh basil.
              </p>

              {/* Quantity / Add */}
              <div className="d-flex justify-content-end">
                <Button variant="outline-primary" size="sm">
                  Add
                </Button>
              </div>
            </Card.Body>
          </Card>
        </Col>

        {/* Example item with quantity */}
        {branchInventories.map((bi) => (
          <Col>
            <Card className="h-100 border-0 shadow-sm">
              <Card.Body>
                <div className="d-flex justify-content-between">
                  <div>
                    <div className="mb-1">
                      <span
                        className="d-inline-block border border-danger rounded-circle me-2"
                        style={{
                          width: 12,
                          height: 12,
                        }}
                      />

                      <strong>{bi.itemName}</strong>
                    </div>

                    <div className="fw-semibold mb-2">₹249</div>
                  </div>

                  <Badge bg="light" text="dark">
                    Main Course
                  </Badge>
                </div>

                <p className="text-muted small mb-3">{bi.description}</p>

                <div className="d-flex justify-content-end">
                  <div className="d-flex align-items-center border rounded">
                    <Button variant="light" size="sm">
                      −
                    </Button>

                    <span className="px-3 fw-semibold">2</span>

                    <Button variant="light" size="sm">
                      +
                    </Button>
                  </div>
                </div>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>

      {/* Sticky Cart */}
      <div
        className="position-fixed bottom-0 start-50 translate-middle-x mb-3"
        style={{
          width: "min(600px, calc(100% - 30px))",
          zIndex: 1000,
        }}
      >
        <Card className="border-0 shadow-lg">
          <Card.Body className="py-2 px-3">
            <div className="d-flex align-items-center justify-content-between">
              <div>
                <strong>2 items</strong>
                <span className="text-muted ms-2">₹498</span>
              </div>

              <Button variant="primary">View Cart →</Button>
            </div>
          </Card.Body>
        </Card>
      </div>
    </Container>
  );
}
