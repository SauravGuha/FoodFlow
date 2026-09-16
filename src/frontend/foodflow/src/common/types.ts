export type RestaurantStatus = "Active" | "Inactive" | "Pending";
export type Category = "undescribed" | "nonveg" | "veg" | "pureveg";

export interface RestaurantOwner {
  name: string;
  email: string;
  phoneNumber: string;
}

export interface Branch {
  id: string;
  name: string;
  phoneNumber: string;
  email: string;
  restaurantId: string;
  address: Address;
  operatingHours: OperatingHours;
  status: string;
}

export interface Address {
  street: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
}

export interface OperatingHours {
  schedule: WeeklySchedule;
}

export interface WeeklySchedule {
  Monday: OperatingTime[];
  Tuesday: OperatingTime[];
  Wednesday: OperatingTime[];
  Thursday: OperatingTime[];
  Friday: OperatingTime[];
  Saturday: OperatingTime[];
  Sunday: OperatingTime[];
}

export interface OperatingTime {
  startTime: string;
  endTime: string;
}

export interface Cuisine {
  name: string;
  restaurantId: string;
  id: string;
  createdAt: string;
  updatedAt: string;
  createdBy: string | null;
  updatedBy: string | null;
}

export interface Restaurant {
  id: string;
  name: string;
  gstNumber: string;
  fNumber: string;
  description: string;
  status: RestaurantStatus;
  createdAt: string;
  restaurantOwner: RestaurantOwner;
  branches: Branch[];
  cuisines: Cuisine[];
}

export interface AddUpdateBranch {
  id?: string;
  restaurantId: string;
  name: string;
  street: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
  phoneNumber: string;
  email: string;
  operatingHours: OperatingHours;
}

export interface UpdateBranchStatus {
  id: string;
  status: string;
}

export interface Item {
  id?: string;
  name: string;
  description: string;
  sku: string;
  restaurantId: string;
  cuisineId: string;
  categoryName: Category;
}

export type RestaurantList = {
  id: string;
  name: string;
};

export type BranchInventoryItem = {
  itemId: string;
  inventoryId: string;
  itemName: string;
  branchId: string;
  quantity: number;
  price: number;
  description: string;
  sku: string;
  category: string;
  cuisineId: string;
  cuisineName: string;
  restaurantId: string;
  restaurantName: string;
  branchName: string;
};

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

export type CartItem = BranchInventoryItem & {
  orderQuantity: number;
};

export type CartSummary = {
  cartItems: CartItem[];
  cartTotal: number;
  branchId: string;
};
