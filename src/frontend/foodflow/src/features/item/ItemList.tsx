import { useContext, useEffect, useState } from "react";
import { Card, Table } from "react-bootstrap";
import { Link, useSearchParams } from "react-router";
import type { Item } from "../../common/types";
import LoaderContext from "../../common/utilities/appContext";
import { getItems } from "../../common/utilities/apiHelper";

export default function ItemList() {
  const [searchParams, setSearchParams] = useSearchParams();
  const restaurantid = searchParams.get("restaurantid");
  const [items, setItems] = useState<Item[]>([]);

  const loaderContext = useContext(LoaderContext);
  const { setLoading, loaderStatus } = loaderContext!;

  useEffect(() => {
    setLoading(true);
    getItems(restaurantid)
      .then((response) => {
        setItems(response.data);
      })
      .finally(() => {
        setLoading(false);
      });
  }, [restaurantid]);

  if (loaderStatus) return <>Loading...</>;

  return (
    <Card>
      <Card.Body>
        <div className="d-flex justify-content-between align-items-center mb-3">
          <Card.Title className="mb-0">Items</Card.Title>

          <Link to="" className="btn btn-primary">
            Add Item
          </Link>
        </div>

        <Table striped bordered hover responsive>
          <thead>
            <tr>
              <th>Name</th>
              <th>Description</th>
              <th>ResturnantId</th>
              <th>CuisineId</th>
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
                  <td>
                    <Link to={``} className="btn btn-sm btn-outline-primary">
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
