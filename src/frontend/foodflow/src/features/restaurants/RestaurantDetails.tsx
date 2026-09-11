import { Tab, Tabs } from "react-bootstrap";
import type { Branch, Cuisine, Restaurant } from "../../common/types";
import { useContext, useEffect, useState } from "react";
import { getRestuarantDetails } from "../../common/utilities/apiHelper";
import LoaderContext from "../../common/utilities/appContext";
import { Link, useParams } from "react-router";

export default function RestaurantDetails() {
  const { id } = useParams();
  if (!id) return <>Id not found...</>;

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
        if (res.data?.branches && res.data?.cuisines) {
          setBranches(res.data.branches);
          setCuisines(res.data.cuisines);
        }
      })
      .finally(() => setLoading(false));
  }, [id]);

  if (!restaurant) return <></>;

  return (
    <Tabs defaultActiveKey="details">
      {/* ---------- Details Tab ---------- */}
      <Tab eventKey="details" title="Details">
        {
          // Card‑style container with a little spacing
          <div className="p-4 border rounded-lg bg-white shadow-sm">
            <Link to={`/restaurants/${restaurant.id}/edit`}>Edit</Link>
            {/* Restaurant name – big heading */}
            <h2 className="text-xl font-bold mb-3">{restaurant.name}</h2>

            {/* Owner contact info */}
            <div className="mb-4">
              <p>
                <strong>Owner:</strong>{" "}
                {restaurant.restaurantOwner?.name ?? "N/A"}
              </p>
              <p>
                <strong>Email:</strong>{" "}
                {restaurant.restaurantOwner?.email ?? "N/A"}
              </p>
              <p>
                <strong>Phone:</strong>{" "}
                {restaurant.restaurantOwner?.phoneNumber ?? "N/A"}
              </p>
            </div>

            {/* GST / F‑Number */}
            <div className="mb-4">
              <p>
                <strong>GST Number:</strong> {restaurant.gstNumber}
              </p>
              <p>
                <strong>F-Number:</strong> {restaurant.fNumber}
              </p>
            </div>

            {/* Description */}
            <div className="mb-3">
              <p>{restaurant.description}</p>
            </div>

            {/* Status badge – Active / Inactive / Pending */}
            <div className="flex items-center justify-between mb-4">
              <span
                className={`inline-flex items-center rounded-full w-6 h-6 ${
                  restaurant.status === "Active"
                    ? "bg-green-500 text-white"
                    : restaurant.status === "Inactive"
                      ? "bg-red-500 text-white"
                      : "bg-yellow-500 text-white"
                }`}
              >
                {restaurant.status}
              </span>
            </div>
          </div>
        }
      </Tab>

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
