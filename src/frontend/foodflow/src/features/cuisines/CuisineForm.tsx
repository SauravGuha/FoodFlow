import { useState } from "react";
import { Button, Card, Form } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router";
import type { Cuisine } from "../../common/types";
import { createCuisine } from "../../common/utilities/apiHelper";

export default function CuisineForm() {
  const { id } = useParams();
  if (!id) {
    return <>Invalid restaurant id</>;
  }
  const [cuisine, setCuisine] = useState<Cuisine>({
    id: "",
    restaurantId: id,
    name: "",
  } as Cuisine);

  const navigate = useNavigate();
  async function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
    event.preventDefault();

    const formData = new FormData(event.currentTarget);
    cuisine.name = formData.get("name") as string;
    await createCuisine(cuisine);
    navigate(`/restaurants/${id}`);
  }

  return (
    <div className="container-fluid">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Add Cuisine</h2>
      </div>

      <Card>
        <Card.Body>
          <Card.Title className="mb-4">Cuisine Information</Card.Title>

          <Form onSubmit={handleSubmit}>
            <Form.Group className="mb-3" controlId="cuisineName">
              <Form.Label>Name</Form.Label>

              <Form.Control
                type="text"
                name="name"
                placeholder="Enter cuisine name"
                required
              />
            </Form.Group>

            <div className="d-flex justify-content-end gap-2 mt-4">
              <Link to={`/restaurants/${id}`} className="btn btn-secondary">
                Cancel
              </Link>

              <Button type="submit" variant="primary">
                Add Cuisine
              </Button>
            </div>
          </Form>
        </Card.Body>
      </Card>
    </div>
  );
}
