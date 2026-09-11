import { useContext, useEffect, useState } from "react";
import { Button, Card, Col, Form, Row } from "react-bootstrap";
import { useParams } from "react-router";
import LoaderContext from "../../common/utilities/appContext";
import { getBranchDetails } from "../../common/utilities/apiHelper";
import type {
  Address,
  Branch,
  OperatingHours,
  OperatingTime,
  WeeklySchedule,
} from "../../common/types";

export default function BranchForm() {
  const { id, branchId } = useParams();
  const [branch, setBranch] = useState<Branch>({
    id: "",
    email: "",
    name: "",
    restaurantId: id,
    phoneNumber: "",
    address: {
      city: "",
      country: "",
      state: "",
      street: "",
      zipCode: "",
    } as Address,
    operatingHours: {
      schedule: {
        Monday: [] as OperatingTime[],
        Tuesday: [] as OperatingTime[],
        Wednesday: [] as OperatingTime[],
        Thursday: [] as OperatingTime[],
        Friday: [] as OperatingTime[],
        Saturday: [] as OperatingTime[],
        Sunday: [] as OperatingTime[],
      } as WeeklySchedule,
    } as OperatingHours,
  } as Branch);
  if (!id) {
    return <>Invalid restaurant id</>;
  }
  const loaderContext = useContext(LoaderContext);
  const { setLoading } = loaderContext!;

  useEffect(() => {
    if (branchId) {
      setLoading(true);
      getBranchDetails(branchId)
        .then((response) => setBranch(response.data))
        .finally(() => setLoading(false));
    }
  }, []);

  function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
    event.preventDefault();

    // API behaviour will be implemented by you.
  }

  return (
    <div className="container-fluid">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>{branchId ? "Update" : "Add"} Branch</h2>
      </div>

      <Card>
        <Card.Body>
          <Form onSubmit={handleSubmit}>
            {/* Branch Information */}
            <Card.Title className="mb-4">Branch Information</Card.Title>

            <Row>
              <Col md={6} className="mb-3">
                <Form.Group controlId="branchName">
                  <Form.Label>Name</Form.Label>
                  <Form.Control
                    type="text"
                    name="name"
                    placeholder="Enter branch name"
                    defaultValue={branch.name}
                  />
                </Form.Group>
              </Col>
            </Row>

            <hr />

            {/* Address */}
            <Card.Title className="mb-4">Address</Card.Title>

            <Row>
              <Col md={12} className="mb-3">
                <Form.Group controlId="street">
                  <Form.Label>Street</Form.Label>
                  <Form.Control
                    type="text"
                    name="street"
                    placeholder="Enter street address"
                    defaultValue={branch.address.street}
                  />
                </Form.Group>
              </Col>

              <Col md={6} className="mb-3">
                <Form.Group controlId="city">
                  <Form.Label>City</Form.Label>
                  <Form.Control
                    type="text"
                    name="city"
                    placeholder="Enter city"
                    defaultValue={branch.address.city}
                  />
                </Form.Group>
              </Col>

              <Col md={6} className="mb-3">
                <Form.Group controlId="state">
                  <Form.Label>State</Form.Label>
                  <Form.Control
                    type="text"
                    name="state"
                    placeholder="Enter state"
                    defaultValue={branch.address.state}
                  />
                </Form.Group>
              </Col>

              <Col md={6} className="mb-3">
                <Form.Group controlId="zipCode">
                  <Form.Label>Zip Code</Form.Label>
                  <Form.Control
                    type="text"
                    name="zipCode"
                    placeholder="Enter zip code"
                    defaultValue={branch.address.zipCode}
                  />
                </Form.Group>
              </Col>

              <Col md={6} className="mb-3">
                <Form.Group controlId="country">
                  <Form.Label>Country</Form.Label>
                  <Form.Control
                    type="text"
                    name="country"
                    placeholder="Enter country"
                    defaultValue={branch.address.country}
                  />
                </Form.Group>
              </Col>
            </Row>

            {/* Actions */}
            <div className="d-flex justify-content-end gap-2 mt-4">
              <Button type="button" variant="secondary">
                Cancel
              </Button>

              <Button type="submit" variant="primary">
                Save Branch
              </Button>
            </div>
          </Form>
        </Card.Body>
      </Card>
    </div>
  );
}
