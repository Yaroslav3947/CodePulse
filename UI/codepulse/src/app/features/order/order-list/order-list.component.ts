import { Component, OnInit } from '@angular/core';
import { OrderService } from '../services/order.service';
import { Order } from '../../blog-post/models/order.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-order-list',
  // standalone: true,
  // imports: [],
  templateUrl: './order-list.component.html',
  styleUrl: './order-list.component.css'
})
export class OrderListComponent implements OnInit {

  orders$?: Observable<Order[]>;

  constructor(private orderService: OrderService) {

  }
  ngOnInit(): void {
    this.orders$ = this.orderService.getAllOrders();
  }
}
