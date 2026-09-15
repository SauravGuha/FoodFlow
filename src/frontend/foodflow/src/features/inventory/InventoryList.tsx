import { useContext, useEffect, useState } from "react";
import { useParams } from "react-router";
import { getBranchInventories } from "../../common/utilities/apiHelper";
import LoaderContext from "../../common/utilities/appContext";
import type { BranchInventoryItem } from "../../common/types";

export default function InventoryList() {
  const { restaurantid, branchid } = useParams();
  const [branchInventories, setBranchInventories] = useState<
    BranchInventoryItem[]
  >([]);
  const loaderContext = useContext(LoaderContext);
  const { setLoading, loaderStatus } = loaderContext!;

  useEffect(() => {
    setLoading(true);
    getBranchInventories(branchid)
      .then((response) => setBranchInventories(response.data))
      .finally(() => setLoading(false));
  }, [branchid]);

  if (loaderStatus) return <>Loading...</>;

  return (
    <div>
      <h2>Inventory List</h2>
      {/* Add your inventory list items here */}
    </div>
  );
}
