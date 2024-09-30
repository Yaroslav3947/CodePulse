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


  getAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`/api/orders`);
  }

  getOrderById(id: string): Observable<Order> {
    return this.http.get<Order>(`/api/orders/${id}`);
  }
  
  updateOrder(id: string, updateOrderRequest: UpdateOrderRequest): Observable<Order> {
    return this.http.put<Order>(`/api/orders/${id}`, updateOrderRequest);
  }

  deleteOrder(id: string): Observable<void> {
    return this.http.delete<void>(`/api/orders/${id}`);
  }
}
