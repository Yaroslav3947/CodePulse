import { OrderItem } from "./order-item";

export interface Order {
    id: string;
    orderDate: Date;
    totalAmount: number;
    status: string;
    userId: string;
    orderItems: OrderItem[];
  }