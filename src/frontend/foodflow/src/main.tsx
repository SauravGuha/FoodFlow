import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "bootstrap/dist/css/bootstrap.min.css";

import App from "./App.tsx";
import { BrowserRouter, Route, Routes } from "react-router";
import Dashboard from "./features/dashboard/Dashboard.tsx";
import RestaurantList from "./features/restaurants/RestaurantList.tsx";
import RestaurantDetails from "./features/restaurants/RestaurantDetails.tsx";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<App />}>
          <Route index element={<Dashboard />} />
          <Route path="restaurants" element={<RestaurantList />} />
          <Route path="restaurants/:id" element={<RestaurantDetails />} />
        </Route>
      </Routes>
    </BrowserRouter>
  </StrictMode>,
);
