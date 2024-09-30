import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../models/order.model';
import { CreateOrderRequest } from '../models/create-order-request';
import { UpdateOrderRequest } from '../models/update-order-request.model';

@Injectable({
  providedIn: 'root'
})
export class OrderServiceService {

  constructor(private http: HttpClient) { }

  getAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`/api/orders`);
  }

  getOrderById(id: string): Observable<Order> {
    return this.http.get<Order>(`/api/orders/${id}`);
  }

  createOrder(orderRequest: CreateOrderRequest): Observable<Order> {
    return this.http.post<Order>(`/api/orders`, orderRequest);
  }

  updateOrder(id: string, updateOrderRequest: UpdateOrderRequest): Observable<Order> {
    return this.http.put<Order>(`/api/orders/${id}`, updateOrderRequest);
  }

  deleteOrder(id: string): Observable<void> {
    return this.http.delete<void>(`/api/orders/${id}`);
  }

  addProductToOrder(orderId: string, productId: string, quantity: number): Observable<Order> {
    return this.http.post<Order>(`/api/orders/${orderId}/add-product/${productId}`, null, { params: { quantity } });
  }

  updateProductQuantityInOrder(orderId: string, productId: string, newQuantity: number): Observable<Order> {
    return this.http.put<Order>(`/api/orders/${orderId}/update-product-quantity/${productId}`, null, { params: { newQuantity } });
  }

  removeProductFromOrder(orderId: string, productId: string): Observable<Order> {
    return this.http.delete<Order>(`/api/orders/${orderId}/remove-product/${productId}`);
  }

  getOrderByUserId(userId: string): Observable<Order> {
    return this.http.get<Order>(`/api/orders/user/${userId}`);
  }
}
