export interface CreateOrderRequest {
    orderDate: Date;
    totalAmount: number;
    status: string;
    userId: string;
    orderItems: string[];
  }
  