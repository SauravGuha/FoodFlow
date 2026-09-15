import axios from "axios";
import type { BranchInventoryItem } from "../types";

const instance = axios.create({
  baseURL: "http://localhost:5243/api",
});

export type CreateBranchInventoryRequest = {
  itemId: string;
  branchId: string;
  quantity: number;
  price: number;
};

export type UpdateBranchStockRequest = {
  itemId: string;
  branchId: string;
  quantity: number;
};

export const getBranchInventories = async function (branchId: string) {
  return await instance.get<BranchInventoryItem>(`/branch/${branchId}/inventory`);
};

export const createBranchInventory = async function (
  data: CreateBranchInventoryRequest,
) {
  return await instance.post(`/item/iteminventory`, data);
};

export const addBranchStock = async function (
  data: UpdateBranchStockRequest,
) {
  return await instance.put(`/item/iteminventory`, data);
};

export const removeBranchStock = async function (
  data: UpdateBranchStockRequest,
) {
  return await instance.put(`/item/removeitembranchstock`, data);
};
