import { Button, Card, Col, Form, Row, Spinner } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router";
import type { Restaurant, RestaurantOwner } from "../../common/types";
import { useContext, useEffect, useState } from "react";
import LoaderContext from "../../common/utilities/appContext";
import {
  addUpdateRestaurant,
  getRestuarantDetails,
} from "../../common/utilities/apiHelper";

export default function RestaurantForm() {
  const { id } = useParams();
  const [isloading, setIsLoading] = useState<boolean>(false);
  const operation = id ? "Update" : "Add";
  const [restaurant, setRestaurant] = useState<Restaurant>({
    id: "",
    name: "",
    gstNumber: "",
    fNumber: "",
    description: "",
    status: "Inactive",
    restaurantOwner: {
      name: "",
      email: "",
      phoneNumber: "",
    } as RestaurantOwner,
  } as Restaurant);
  const loaderContext = useContext(LoaderContext);
  const { setLoading } = loaderContext!;

  useEffect(() => {
    if (id) {
      setLoading(true);
      setIsLoading(true);
      getRestuarantDetails(id)
        .then((res) => {
          setRestaurant(res.data);
        })
        .finally(() => {
          setLoading(false);
          setIsLoading(false);
        });
    }
  }, []);

  const navigate = useNavigate();
  const [submitting, setSubmitting] = useState<boolean>(false);
  async function handleSubmit(e: React.SubmitEvent<HTMLFormElement>) {
    e.preventDefault();
    const formData = new FormData(e.target);
    restaurant.name = formData.get("name")!.toString();
    restaurant.gstNumber = formData.get("gst")!.toString();
    restaurant.fNumber = formData.get("fNumber")!.toString();
    restaurant.description = formData.get("description")!.toString();
    restaurant.restaurantOwner.name = formData.get("ownerName")!.toString();
    restaurant.restaurantOwner.email = formData.get("ownerEmail")!.toString();
    restaurant.restaurantOwner.phoneNumber = formData
      .get("ownerPhone")!
      .toString();
    setSubmitting(true);
    await addUpdateRestaurant(restaurant);
    setSubmitting(false);
    if (restaurant.id) {
      navigate(`/restaurants/${restaurant.id}`);
    } else {
      navigate("/restaurants");
    }
  }

  if (isloading) return <></>;

  return (
    <div className="container-fluid">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Add Restaurant</h2>
      </div>

      <Card>
        <Card.Body>
          <Form onSubmit={(e) => handleSubmit(e)}>
            <input hidden defaultValue={restaurant.id} />
            {/* Restaurant Information */}
            <Card.Title className="mb-4">Restaurant Information</Card.Title>

            <Row>
              <Col md={6} className="mb-3">
                <Form.Group controlId="restaurantName">
                  <Form.Label>Name</Form.Label>
                  <Form.Control
                    type="text"
                    name="name"
                    placeholder="Enter restaurant name"
                    defaultValue={restaurant.name}
                  />
                </Form.Group>
              </Col>

              <Col md={6} className="mb-3">
                <Form.Group controlId="gstNumber">
                  <Form.Label>GST Number</Form.Label>
                  <Form.Control
                    type="text"
                    name="gst"
                    placeholder="Enter GST number"
                    defaultValue={restaurant.gstNumber}
                  />
                </Form.Group>
              </Col>

              <Col md={6} className="mb-3">
                <Form.Group controlId="fNumber">
                  <Form.Label>F Number</Form.Label>
                  <Form.Control
                    type="text"
                    name="fNumber"
                    placeholder="Enter F number"
                    defaultValue={restaurant.fNumber}
                  />
                </Form.Group>
              </Col>

              <Col md={12} className="mb-3">
                <Form.Group controlId="description">
                  <Form.Label>Description</Form.Label>
                  <Form.Control
                    as="textarea"
                    rows={3}
                    name="description"
                    placeholder="Enter restaurant description"
                    defaultValue={restaurant.description}
                  />
                </Form.Group>
              </Col>
            </Row>

            <hr />

            {/* Restaurant Owner */}
            <Card.Title className="mb-4">Restaurant Owner</Card.Title>

            <Row>
              <Col md={6} className="mb-3">
                <Form.Group controlId="ownerName">
                  <Form.Label>Name</Form.Label>
                  <Form.Control
                    type="text"
                    name="ownerName"
                    placeholder="Enter owner name"
                    defaultValue={restaurant.restaurantOwner.name}
                  />
                </Form.Group>
              </Col>

              <Col md={6} className="mb-3">
                <Form.Group controlId="ownerEmail">
                  <Form.Label>Email</Form.Label>
                  <Form.Control
                    type="email"
                    name="ownerEmail"
                    placeholder="Enter owner email"
                    defaultValue={restaurant.restaurantOwner.email}
                  />
                </Form.Group>
              </Col>

              <Col md={6} className="mb-3">
                <Form.Group controlId="ownerPhone">
                  <Form.Label>Phone Number</Form.Label>
                  <Form.Control
                    type="text"
                    name="ownerPhone"
                    placeholder="Enter phone number"
                    defaultValue={restaurant.restaurantOwner.phoneNumber}
                  />
                </Form.Group>
              </Col>
            </Row>

            {/* Actions */}
            <div className="d-flex justify-content-end gap-2 mt-4">
              <Link
                to={`/restaurants/${restaurant.id}`}
                className="btn btn-secondary"
              >
                Cancel
              </Link>

              <Button type="submit" variant="primary" disabled={submitting}>
                {submitting ? <Spinner size="sm" /> : <></>}
                Save
              </Button>
            </div>
          </Form>
        </Card.Body>
      </Card>
    </div>
  );
}
