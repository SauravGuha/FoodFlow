import { useContext, useEffect, useState } from "react";
import {
  Badge,
  Button,
  Card,
  Col,
  Container,
  Form,
  Row,
} from "react-bootstrap";
import type { Restaurant } from "../../common/types";
import LoaderContext from "../../common/utilities/appContext";
import { getRestaurants } from "../../common/utilities/apiHelper";
import { useNavigate } from "react-router";

export default function RestaurantList() {
  const [restaurants, setRestaurants] = useState<Restaurant[]>([]);
  const loaderContext = useContext(LoaderContext);
  const { setLoading, loaderStatus } = loaderContext!;
  const navigate = useNavigate();

  useEffect(() => {
    setLoading(true);
    getRestaurants()
      .then((response) => {
        setRestaurants(response.data);
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  if (loaderStatus) return <>Loading...</>;

  function handleViewRestaurant(id: string): void {
    navigate(`/customer/restaurants/${id}/branches`);
  }

  return (
    <Container fluid className="py-4">
      {/* Header */}
      <div className="mb-4">
        <h3 className="mb-1">Choose a Restaurant</h3>
        <p className="text-muted mb-0">Select a restaurant to view its menu</p>
      </div>

      {/* Search */}
      <Form.Control
        id="search"
        type="search"
        placeholder="Search restaurants..."
        className="mb-4"
      />

      {/* Restaurant List */}
      <Row xs={1} md={2} lg={3} className="g-4">
        {/* Restaurant Card */}
        {restaurants.map((r) => (
          <Col>
            <Card className="h-100 shadow-sm border-0">
              <Card.Body>
                <div className="d-flex justify-content-between mb-2">
                  <div>
                    <h5 className="mb-1">{r.name}</h5>
                    <small className="text-muted">Indian • Bengali</small>
                  </div>

                  <Badge bg="success">Open</Badge>
                </div>

                <p className="text-muted small mb-3">{r.description}</p>

                <div className="small text-muted mb-3">
                  {r.branches.length} branches available
                </div>

                <Button
                  variant="primary"
                  className="w-100"
                  onClick={() => {
                    handleViewRestaurant(r.id);
                  }}
                >
                  View Restaurant
                </Button>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </Container>
  );
}
