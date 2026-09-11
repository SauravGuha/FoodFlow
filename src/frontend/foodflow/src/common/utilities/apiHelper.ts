import axios from "axios";
import type { Restaurant } from "../types";

const delayer = function (value: number) {
  return new Promise((resolve) => {
    setTimeout(() => resolve(value), value * 1000);
  });
};

const instance = axios.create({
  baseURL: "http://localhost:5243/api",
});

instance.interceptors.request.use(
  async function (config) {
    await delayer(2);
    // Do something before request is sent
    return config;
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error);
  },
);

export const getRestaurants = async function () {
  return await instance.get<Restaurant[]>("/restaurant/filtered");
};

export const getRestuarantDetails = async function (id: string) {
  return await instance.get<Restaurant>(`/restaurant/${id}`);
};

export const addUpdateRestaurant = async function (data: Restaurant) {
  if (data.id) {
    return await instance.put(`/restaurant`, data);
  } else {
    return await instance.post(`/restaurant`, data);
  }
};
