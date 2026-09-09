import BranchList from "../../features/branches/BranchList";
import CuisineList from "../../features/cuisines/CuisineList";
import Dashboard from "../../features/dashboard/Dashboard";
import InventoryList from "../../features/inventory/InventoryList";
import RestaurantList from "../../features/restaurants/RestaurantList";

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
