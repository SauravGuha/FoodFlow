import { useContext, useEffect, useState } from "react";
import { Button, Card, Col, Form, Row, Table } from "react-bootstrap";
import { Link, useNavigate, useSearchParams } from "react-router";
import type { Cuisine, Item, RestaurantList } from "../../../common/types";
import LoaderContext from "../../../common/utilities/appContext";
import {
  getItems,
  getRestaurantCuisines,
  getRestaurantList,
} from "../../../common/utilities/apiHelper";

export default function ItemList() {
  const [searchParams, setSearchParams] = useSearchParams();
  const [showFilters, setShowFilters] = useState(false);
  const restaurantid = searchParams.get("restaurantid");
  const cuisineid = searchParams.get("cuisineid");
  const categoryname = searchParams.get("categoryname");
  const [items, setItems] = useState<Item[]>([]);
  const [restaurants, setRestaurants] = useState<RestaurantList[]>([]);
  const [cuisines, setCuisines] = useState<Cuisine[]>([]);
  const categories = ["undescribed", "nonveg", "veg", "pureveg"];

  const loaderContext = useContext(LoaderContext);
  const { setLoading, loaderStatus } = loaderContext!;

  useEffect(() => {
    setLoading(true);
    const promiseArray = [];
    promiseArray.push(
      getItems(restaurantid, cuisineid, categoryname).then((response) => {
        setItems(response.data);
      }),
    );
    promiseArray.push(
      getRestaurantList().then((response) => setRestaurants(response.data)),
    );
    if (restaurantid) {
      promiseArray.push(
        getRestaurantCuisines(restaurantid).then((response) => {
          setCuisines(response.data);
        }),
      );
    }
    Promise.all(promiseArray).finally(() => {
      setLoading(false);
    });
  }, [restaurantid, cuisineid, categoryname]);

  const navigate = useNavigate();
  async function applyHandle(e: React.MouseEvent<HTMLButtonElement>) {
    e.preventDefault();
    const filterButtonName = (e.target as HTMLButtonElement).innerText;
    if (filterButtonName.toUpperCase() == "APPLY") {
      let url = "/items";
      const restaurant = document.querySelector(
        "#restaurantFilter",
      ) as HTMLSelectElement;
      //get the selected option
      const restaurantId = restaurant.value;
      if (restaurantId) {
        url = url + `?restaurantid=${restaurantId}`;
      }
      const cuisine = document.querySelector(
        "#cuisineFilter",
      ) as HTMLSelectElement;
      //get the selected option
      const cuisineId = cuisine.value;
      if (cuisineId) {
        if (url.includes("?")) {
          url = url + `&cuisineid=${cuisineId}`;
        } else {
          url = url + `?cuisineid=${cuisineId}`;
        }
      }
      const category = document.querySelector(
        "#categoryFilter",
      ) as HTMLSelectElement;
      //get the selected option
      const categoryName = category.value;
      if (categoryName) {
        if (url.includes("?")) {
          url = url + `&categoryname=${categoryName}`;
        } else {
          url = url + `?categoryname=${categoryName}`;
        }
      }
      navigate(url);
    }
    if (filterButtonName.toUpperCase() == "CLEAR") {
      setShowFilters(false);
      navigate(`/items`);
    }
  }

  if (loaderStatus) return <>Loading...</>;

  function handleRestaurantChange(
    event: React.ChangeEvent<HTMLSelectElement, HTMLSelectElement>,
  ): void {
    event.preventDefault();
    const restaurantId = (event.target as HTMLSelectElement).value;
    getRestaurantCuisines(restaurantId).then((response) => {
      setCuisines(response.data);
    });
  }

  return (
    <Card>
      <Card.Body>
        <div className="d-flex justify-content-between align-items-center mb-3">
          <Card.Title className="mb-0">Items</Card.Title>

          <div className="d-flex gap-2">
            <Button
              variant="outline-secondary"
              onClick={() => setShowFilters((value) => !value)}
            >
              Filter
            </Button>

            <Link to="/items/new" className="btn btn-primary">
              Add Item
            </Link>
          </div>
        </div>

        {showFilters && (
          <Card className="mb-3">
            <Card.Body>
              <Row>
                <Col md={4}>
                  <Form.Group controlId="restaurantFilter">
                    <Form.Label>Restaurant</Form.Label>

                    <Form.Select
                      name="restaurantId"
                      onChange={handleRestaurantChange}
                    >
                      <option value="">All Restaurants</option>

                      {restaurants.map((restaurant) => (
                        <option
                          key={restaurant.id}
                          value={restaurant.id}
                          selected={restaurant.id == restaurantid}
                        >
                          {restaurant.name}
                        </option>
                      ))}
                    </Form.Select>
                  </Form.Group>
                </Col>

                <Col md={4}>
                  <Form.Group controlId="cuisineFilter">
                    <Form.Label>Cuisine</Form.Label>

                    <Form.Select name="cuisineId">
                      <option value="">All Cuisines</option>

                      {cuisines.map((cuisine) => (
                        <option
                          key={cuisine.id}
                          value={cuisine.id}
                          selected={cuisine.id == cuisineid}
                        >
                          {cuisine.name}
                        </option>
                      ))}
                    </Form.Select>
                  </Form.Group>
                </Col>

                <Col md={4}>
                  <Form.Group controlId="categoryFilter">
                    <Form.Label>Category</Form.Label>

                    <Form.Select name="category">
                      <option value="">All Categories</option>

                      {categories.map((cat) => (
                        <option
                          value={cat}
                          key={cat}
                          selected={cat == categoryname}
                        >
                          {cat}
                        </option>
                      ))}
                    </Form.Select>
                  </Form.Group>
                </Col>
              </Row>

              <div className="d-flex justify-content-end gap-2 mt-3">
                <Button variant="secondary" onClick={applyHandle}>
                  Clear
                </Button>

                <Button variant="primary" onClick={applyHandle}>
                  Apply
                </Button>
              </div>
            </Card.Body>
          </Card>
        )}

        <Table striped bordered hover responsive>
          <thead>
            <tr>
              <th>Name</th>
              <th>Description</th>
              <th>ResturnantId</th>
              <th>CuisineId</th>
              <th>Category</th>
              <th>Action</th>
            </tr>
          </thead>

          <tbody>
            {items.length === 0 ? (
              <tr>
                <td colSpan={5} className="text-center">
                  No items found
                </td>
              </tr>
            ) : (
              items.map((item) => (
                <tr key={item.id}>
                  <td>{item.name}</td>
                  <td>{item.description}</td>
                  <td>{item.restaurantId}</td>
                  <td>{item.cuisineId}</td>
                  <td>{item.categoryName}</td>
                  <td>
                    <Link
                      to={`/items/${item.id}/edit`}
                      className="btn btn-sm btn-outline-primary"
                    >
                      Edit
                    </Link>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </Table>
      </Card.Body>
    </Card>
  );
}
