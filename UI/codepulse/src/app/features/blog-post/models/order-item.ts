import { Product } from "./product.model";

export interface OrderItem {
  productId: string;
  product: Product; 
  quantity: number;
  price: number;
  totalCost: number;
}