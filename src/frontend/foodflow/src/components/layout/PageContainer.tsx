import BranchList from "../../features/admin/branches/BranchList";
import CuisineList from "../../features/admin/cuisines/CuisineList";
import Dashboard from "../../features/admin/dashboard/Dashboard";
import InventoryList from "../../features/admin/inventory/InventoryList";
import RestaurantList from "../../features/admin/restaurants/Restaurants";

type PageContainerProps = {
  feature: string;
};

export default function PageContainer({ feature }: PageContainerProps) {
  let displayFeature = null;
  switch (feature.toLocaleLowerCase()) {
    case "restaurants":
      displayFeature = <RestaurantList />;
      break;

    case "branches":
      displayFeature = <BranchList />;
      break;

    case "cuisines":
      displayFeature = <CuisineList />;
      break;

    case "inventory":
      displayFeature = <InventoryList />;
      break;

    default:
      displayFeature = <Dashboard />;
      break;
  }
  return <div className="page-container">{displayFeature}</div>;
}
