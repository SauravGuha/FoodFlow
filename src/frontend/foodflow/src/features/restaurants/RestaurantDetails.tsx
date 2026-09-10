import { Tab, Tabs } from "react-bootstrap";
import type { Branch, Cuisine, Restaurant } from "../../common/types";
import { useContext, useEffect, useState } from "react";
import { getRestuarantDetails } from "../../common/utilities/apiHelper";
import LoaderContext from "../../common/utilities/appContext";

export default function RestaurantDetails({ id }: { id: string }) {
  const [restaurant, setRestaurant] = useState<Restaurant | undefined>(
    undefined,
  );
  const loaderContext = useContext(LoaderContext);
  const { setLoading } = loaderContext!;

  // State for branch and cuisine lists
  const [branches, setBranches] = useState<Branch[]>([]);
  const [cuisines, setCuisines] = useState<Cuisine[]>([]);

  useEffect(() => {
    setLoading(true);
    getRestuarantDetails(id)
      .then((res) => {
        setRestaurant(res.data);
        // Populate branch and cuisine lists from the fetched restaurant data
        if (restaurant?.branches && restaurant?.cuisines) {
          setBranches(restaurant.branches);
          setCuisines(restaurant.cuisines);
        }
      })
      .finally(() => setLoading(false));
  }, [id]);

  return (
    <Tabs defaultActiveKey="details">
      <Tab eventKey="details" title="Details"></Tab>

      <Tab eventKey="branches" title="Branches">
        <div>
          {branches.length === 0 ? (
            <p>No branches found.</p>
          ) : (
            <>
              <table className="table table-striped">
                <thead>
                  <tr>
                    <th>Name</th>
                    <th>Address</th>
                    <th>Hours</th>
                  </tr>
                </thead>
                <tbody>
                  {branches.map((b) => (
                    <tr key={b.id}>
                      <td>{b.name}</td>
                      <td>
                        {b.address.street}, {b.address.city},{" "}
                        {b.address.zipCode} ,{b.address.state} ,{" "}
                        {b.address.country}
                      </td>
                      <td>
                        {/* {b.operatingHours.schedule.Monday.startTime} –{" "}
                        {b.operatingHours.schedule.Monday.endTime} */}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </>
          )}
        </div>
      </Tab>

      <Tab eventKey="cuisines" title="Cuisines">
        <div>
          {cuisines.length === 0 ? (
            <p>No cuisines found.</p>
          ) : (
            <>
              <table className="table table-striped">
                <thead>
                  <tr>
                    <th>Name</th>
                    <th>Restaurant ID</th>
                    <th>Created At</th>
                    <th>Updated At</th>
                  </tr>
                </thead>
                <tbody>
                  {cuisines.map((c) => (
                    <tr key={c.id}>
                      <td>{c.name}</td>
                      <td>{c.restaurantId}</td>
                      <td>{c.createdAt}</td>
                      <td>{c.updatedAt}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </>
          )}
        </div>
      </Tab>
    </Tabs>
  );
}
