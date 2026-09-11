import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "bootstrap/dist/css/bootstrap.min.css";

import App from "./App.tsx";
import { BrowserRouter, Route, Routes } from "react-router";
import Dashboard from "./features/dashboard/Dashboard.tsx";
import RestaurantList from "./features/restaurants/RestaurantList.tsx";
import RestaurantDetails from "./features/restaurants/RestaurantDetails.tsx";
import RestaurantForm from "./features/restaurants/RestaurantForm.tsx";
import BranchForm from "./features/branches/BranchForm.tsx";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<App />}>
          <Route index element={<Dashboard />} />
          <Route path="restaurants" element={<RestaurantList />} />
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
        </Route>
      </Routes>
    </BrowserRouter>
  </StrictMode>,
);
