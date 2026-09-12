export type RestaurantStatus = "Active" | "Inactive" | "Pending";

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
