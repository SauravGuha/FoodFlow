import { useContext, useEffect, useState } from "react";
import { Button, Card, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router";
import LoaderContext from "../../common/utilities/appContext";
import {
  addUpdateBranchDetails,
  getBranchDetails,
  updateBranchStatus,
} from "../../common/utilities/apiHelper";
import type {
  Address,
  AddUpdateBranch,
  Branch,
  OperatingHours,
  OperatingTime,
  WeeklySchedule,
} from "../../common/types";
import SaveButton from "../../components/common/SaveButton";

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
  const [isloading, setIsLoading] = useState<boolean>(false);

  useEffect(() => {
    if (branchId) {
      setLoading(true);
      setIsLoading(true);
      getBranchDetails(branchId)
        .then((response) => setBranch(response.data))
        .finally(() => {
          setLoading(false);
          setIsLoading(false);
        });
    }
  }, []);

  const navigate = useNavigate();
  const [submitting, setSubmitting] = useState<boolean>(false);
  async function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
    event.preventDefault();

    // API behaviour will be implemented by you.
    const formData = new FormData(event.target);
    const branchObject = {
      name: formData.get("name")!.toString(),
      city: formData.get("city")!.toString(),
      state: formData.get("state")!.toString(),
      street: formData.get("street")!.toString(),
      zipCode: formData.get("zipCode")!.toString(),
      country: formData.get("country")!.toString(),
      id: branch.id,
      operatingHours: branch.operatingHours,
      restaurantId: id,
      email: formData.get("email")!.toString(),
      phoneNumber: formData.get("phoneNumber")!.toString(),
    } as AddUpdateBranch;

    // Add the rest of the fields to the branch object.
    setSubmitting(true);
    await addUpdateBranchDetails(branchObject);
    setSubmitting(false);
    navigate("/restaurants/" + id);
  }

  async function handleStatusChange(
    event: React.ChangeEvent<HTMLInputElement>,
  ) {
    const newStatus = event.target.checked ? "Active" : "Inactive";
    await updateBranchStatus({ id: branch.id, status: newStatus });
    setBranch((prevBranch) => ({
      ...prevBranch,
      status: newStatus,
    }));
  }

  if (isloading) {
    return <>Loading...</>;
  }

  const operatingHoursSchedule = Object.entries(branch.operatingHours.schedule);

  return (
    <div className="container-fluid">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>{branchId ? "Update" : "Add"} Branch</h2>
      </div>

      {branchId && (
        <Col md={6} className="mb-3">
          <Form.Group controlId="branchStatus">
            <Form.Label>Status</Form.Label>

            <Form.Check
              type="switch"
              id="branch-status"
              name="status"
              label={branch.status === "Active" ? "Active" : "Inactive"}
              defaultChecked={branch.status === "Active"}
              onChange={handleStatusChange}
            />
          </Form.Group>
        </Col>
      )}

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
              <Col md={6} className="mb-3">
                <Form.Group controlId="phoneNumber">
                  <Form.Label>Phone Number</Form.Label>
                  <Form.Control
                    type="text"
                    name="phoneNumber"
                    placeholder="Enter branch phone number"
                    defaultValue={branch.phoneNumber}
                  />
                </Form.Group>
              </Col>
              <Col md={6} className="mb-3">
                <Form.Group controlId="email">
                  <Form.Label>Email</Form.Label>
                  <Form.Control
                    type="text"
                    name="email"
                    placeholder="Enter branch email"
                    defaultValue={branch.email}
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

            <hr />

            <Card.Title className="mb-4">Operating Hours</Card.Title>

            {operatingHoursSchedule.map((day) => (
              <Row key={day[0]} className="align-items-end mb-3">
                <Col md={3}>
                  <Form.Label>{day[0]}</Form.Label>
                </Col>

                <Col md={4}>
                  <Form.Group controlId={`${day[0]}-startTime`}>
                    <Form.Label>Opening Time</Form.Label>
                    <Form.Control
                      type="time"
                      name={`operatingHours.${day[0]}.startTime`}
                      defaultValue={day[1][0]?.startTime}
                    />
                  </Form.Group>
                </Col>

                <Col md={4}>
                  <Form.Group controlId={`${day[0]}-endTime`}>
                    <Form.Label>Closing Time</Form.Label>
                    <Form.Control
                      type="time"
                      name={`operatingHours.${day[0]}.endTime`}
                      defaultValue={day[1][0]?.endTime}
                    />
                  </Form.Group>
                </Col>

                <Col md={1}>
                  <Button
                    type="button"
                    variant="outline-primary"
                    title={`Add another time period for ${day}`}
                  >
                    +
                  </Button>
                </Col>
              </Row>
            ))}

            {/* Actions */}
            <div className="d-flex justify-content-end gap-2 mt-4">
              <Link to={`/restaurants/${id}`} className="btn btn-secondary">
                Cancel
              </Link>

              <SaveButton submitting={submitting} />
            </div>
          </Form>
        </Card.Body>
      </Card>
    </div>
  );
}
