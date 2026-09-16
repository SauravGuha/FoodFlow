import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "bootstrap/dist/css/bootstrap.min.css";

import App from "./App.tsx";
import { BrowserRouter, Route, Routes } from "react-router";
import Dashboard from "./features/admin/dashboard/Dashboard.tsx";
import RestaurantDetails from "./features/admin/restaurants/RestaurantDetails.tsx";
import RestaurantForm from "./features/admin/restaurants/RestaurantForm.tsx";
import BranchForm from "./features/admin/branches/BranchForm.tsx";
import CuisineForm from "./features/admin/cuisines/CuisineForm.tsx";
import ItemList from "./features/admin/item/ItemList.tsx";
import ItemForm from "./features/admin/item/ItemForm.tsx";
import InventoryList from "./features/admin/inventory/InventoryList.tsx";
import Restaurants from "./features/admin/restaurants/Restaurants.tsx";
import RestaurantList from "./features/customer/RestaurantList.tsx";
import BranchList from "./features/customer/BranchList.tsx";
import BranchMenu from "./features/customer/Menu/BranchMenu.tsx";
import Cart from "./features/customer/Cart/Cart.tsx";
import keycloak from "./common/auth/keycloak.ts";

await keycloak.init({
  //onLoad: "login-required",
  onLoad: "check-sso",
  pkceMethod: "S256",
});

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<App />}>
            <Route index element={<Dashboard />} />
            <Route path="restaurants" element={<Restaurants />} />
            <Route path="restaurants/:id" element={<RestaurantDetails />} />
            <Route path="restaurants/new" element={<RestaurantForm />} />
            <Route path="restaurants/:id/edit" element={<RestaurantForm />} />
            <Route
              path="/restaurants/:id/branches/new"
              element={<BranchForm />}
            />
            <Route
              path="/restaurants/:id/branches/:branchId?"
              element={<BranchForm />}
            />
            <Route
              path="/restaurants/:id/cuisines/new"
              element={<CuisineForm />}
            />
            <Route path="items" element={<ItemList />} />
            <Route path="items/new" element={<ItemForm />} />
            <Route path="items/:id/edit" element={<ItemForm />} />
            <Route
              path="restaurants/:restaurantid/branches/:branchid/inventoryitems"
              element={<InventoryList />}
            />
            <Route path="/customer/restaurants" element={<RestaurantList />} />
            <Route
              path="/customer/restaurants/:id/branches"
              element={<BranchList />}
            />
            <Route path="/customer/menu/:branchid" element={<BranchMenu />} />
            <Route path="/customer/cart" element={<Cart />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </>
  </StrictMode>,
);
