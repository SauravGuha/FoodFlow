import { useContext, useEffect, useState } from "react";
import type { Restaurant } from "../../common/types";
import { getRestaurants } from "../../common/utilities/apiHelper";
import LoaderContext from "../../common/utilities/appContext";
import { Table } from "react-bootstrap";
import { Link } from "react-router";

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
    <>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Restaurants</h2>
        <Link to="/restaurants/new" className="btn btn-primary">
          Add Restaurant
        </Link>
      </div>
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
            <tr key={r.id}>
              <td>{r.name}</td>
              <td>{r.description}</td>
              <td>{r.status}</td>
              <td>{r.restaurantOwner.name}</td>
              <td>
                <Link to={`${r.id}`}>View</Link>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </>
  );
}
