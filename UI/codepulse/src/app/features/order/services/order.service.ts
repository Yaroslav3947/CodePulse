import { Injectable } from '@angular/core';
import { Order } from '../../blog-post/models/order.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UpdateOrderRequest } from '../../blog-post/models/update-order-request.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) { }


  getOrderByUserId(userId: string): Observable<Order> {
    return this.http.get<Order>(`/api/orders/user/${userId}`);
  }

  getAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`/api/orders?addAuth=true`);
  }

  getOrderById(id: string): Observable<Order> {
    return this.http.get<Order>(`/api/orders/${id}`);
  }
  
  updateOrder(id: string, updateOrderRequest: UpdateOrderRequest): Observable<Order> {
    return this.http.put<Order>(`/api/orders/${id}`, updateOrderRequest);
  }

  deleteOrder(id: string): Observable<void> {
    return this.http.delete<void>(`/api/orders/${id}?addAuth=true`);
  }

  addProductToOrder(orderId: string, productId: string, quantity: number): Observable<Order> {
    return this.http.post<Order>(`/api/orders/${orderId}/add-product/${productId}`, null, { params: { quantity } });
  }

  updateProductQuantityInOrder(orderId: string, productId: string, newQuantity: number): Observable<Order> {
    return this.http.put<Order>(`/api/orders/${orderId}/update-product-quantity/${productId}`, null, { params: { newQuantity } });
  }
}
