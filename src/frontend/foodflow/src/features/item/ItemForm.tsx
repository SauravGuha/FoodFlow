import { useContext, useEffect, useState } from "react";
import { Button, Card, Col, Form, Row } from "react-bootstrap";
import { useNavigate, useParams } from "react-router";
import type { Cuisine, Item, RestaurantList } from "../../common/types";
import {
  createRestaurantItem,
  getItemDetails,
  getRestaurantCuisines,
  getRestaurantList,
} from "../../common/utilities/apiHelper";
import LoaderContext from "../../common/utilities/appContext";

export default function ItemForm() {
  const { id } = useParams();
  const isEditMode = id ? "Edit" : "Add";
  const categories = ["undescribed", "nonveg", "veg", "pureveg"];
  const [restaurants, setRestaurants] = useState<RestaurantList[]>([]);
  const [cuisines, setCuisines] = useState<Cuisine[]>([]);
  const loaderContext = useContext(LoaderContext);
  const { setLoading, loaderStatus } = loaderContext!;
  const [item, setItem] = useState<Item>({
    id: "",
    name: "",
    categoryName: "undescribed",
    description: "",
    sku: "",
    cuisineId: "",
    restaurantId: "",
  } as Item);

  useEffect(() => {
    getRestaurantList()
      .then((response) => setRestaurants(response.data))
      .catch((err) => alert(err));
  }, []);

  useEffect(() => {
    if (id) {
      setLoading(true);
      getItemDetails(id)
        .then((res) => {
          setItem(res.data);
          getRestaurantCuisines(res.data.restaurantId).then((response) =>
            setCuisines(response.data),
          );
        })
        .finally(() => {
          setLoading(false);
        });
    }
  }, []);

  const navigate = useNavigate();

  async function handleSubmit(e: React.SubmitEvent<HTMLFormElement>) {
    e.preventDefault();
    const formData = new FormData(e.target);
    for (const [key, value] of formData.entries()) {
      item[key] = value;
    }
    await createRestaurantItem(item);
    navigate("/items");
  }

  if (loaderStatus) return <>Loading...</>;

  function handleRestaurantChange(
    e: React.ChangeEvent<HTMLSelectElement, HTMLSelectElement>,
  ): void {
    e.preventDefault();
    getRestaurantCuisines((e.target as HTMLSelectElement).value).then(
      (response) => setCuisines(response.data),
    );
  }

  return (
    <Card>
      <Card.Body>
        <Card.Title>{`${isEditMode} Item`}</Card.Title>

        <Form onSubmit={handleSubmit}>
          <Row>
            <Col md={6}>
              <Form.Group className="mb-3" controlId="itemName">
                <Form.Label>Name</Form.Label>
                <Form.Control
                  type="text"
                  name="name"
                  defaultValue={item.name}
                  placeholder="Enter item name"
                  required
                />
              </Form.Group>
            </Col>

            <Col md={6}>
              <Form.Group className="mb-3" controlId="itemSku">
                <Form.Label>SKU</Form.Label>
                <Form.Control
                  type="text"
                  name="sku"
                  defaultValue={item.sku}
                  placeholder="Enter SKU"
                  required
                />
              </Form.Group>
            </Col>
          </Row>

          <Row>
            <Col md={12}>
              <Form.Group className="mb-3" controlId="itemDescription">
                <Form.Label>Description</Form.Label>
                <Form.Control
                  as="textarea"
                  rows={3}
                  name="description"
                  defaultValue={item.description}
                  placeholder="Enter item description"
                  required
                />
              </Form.Group>
            </Col>
          </Row>

          <Row>
            <Col md={4}>
              <Form.Group className="mb-3" controlId="itemRestaurant">
                <Form.Label>Restaurant</Form.Label>
                <Form.Select
                  name="restaurantId"
                  defaultValue={item.restaurantId}
                  required
                  disabled={item.id ? true : false}
                  onChange={handleRestaurantChange}
                >
                  <option value="">Select restaurant</option>

                  {restaurants.map((restaurant) => (
                    <option
                      key={restaurant.id}
                      value={restaurant.id}
                      selected={restaurant.id == item.restaurantId}
                    >
                      {restaurant.name}
                    </option>
                  ))}
                </Form.Select>
              </Form.Group>
            </Col>

            <Col md={4}>
              <Form.Group className="mb-3" controlId="itemCuisine">
                <Form.Label>Cuisine</Form.Label>
                <Form.Select
                  name="cuisineId"
                  defaultValue={item.cuisineId}
                  required
                >
                  <option value="">Select cuisine</option>

                  {cuisines.map((cuisine) => (
                    <option
                      key={cuisine.id}
                      value={cuisine.id}
                      selected={cuisine.id == item.cuisineId}
                    >
                      {cuisine.name}
                    </option>
                  ))}
                </Form.Select>
              </Form.Group>
            </Col>

            <Col md={4}>
              <Form.Group className="mb-3" controlId="itemCategory">
                <Form.Label>Category</Form.Label>
                <Form.Select
                  name="categoryName"
                  defaultValue={item.categoryName}
                  required
                >
                  <option value="">Select category</option>

                  {categories.map((category) => (
                    <option key={category} value={category}>
                      {category}
                    </option>
                  ))}
                </Form.Select>
              </Form.Group>
            </Col>
          </Row>

          <div className="d-flex justify-content-end gap-2 mt-3">
            <Button
              variant="secondary"
              type="button"
              onClick={() => {
                navigate("/items");
              }}
            >
              Cancel
            </Button>

            <Button variant="primary" type="submit">
              {isEditMode}
            </Button>
          </div>
        </Form>
      </Card.Body>
    </Card>
  );
}
