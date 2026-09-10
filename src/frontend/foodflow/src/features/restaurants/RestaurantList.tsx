import { useContext, useEffect, useState } from "react";
import type { Restaurant } from "../../common/types";
import { getRestaurants } from "../../common/utilities/apiHelper";
import LoaderContext from "../../common/utilities/appContext";
import { Button, Table } from "react-bootstrap";

export default function RestaurantList() {
  const [restaurants, setRestaurants] = useState<Restaurant[]>([]);
  const loaderContext = useContext(LoaderContext);
  const { setLoading } = loaderContext!;

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

  return (
    <Table striped bordered hover>
      <thead>
        <tr>
          <th>Name</th>
          <th>Description</th>
          <th>Status</th>
          <th>Owner</th>
          <th>Actions</th>
        </tr>
      </thead>

      <tbody>
        {restaurants.map((r) => (
          <tr>
            <td>{r.name}</td>
            <td>{r.description}</td>
            <td>{r.status}</td>
            <td>{r.restaurantOwner.name}</td>
            <td>
              <Button size="sm">Edit</Button>
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
}
