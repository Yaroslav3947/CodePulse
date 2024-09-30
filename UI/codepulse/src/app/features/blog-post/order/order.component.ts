import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { AuthService } from 'src/app/features/auth/services/auth.service';
import { UserModel } from 'src/app/features/auth/models/user.model';
import { Order } from '../models/order.model';
import { OrderServiceService } from '../services/order.service.service';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderDetailComponent implements OnInit, OnDestroy {

  order$?: Observable<Order>;
  order?: Order;
  paramsSubscription?: Subscription;
  orderSubscription?: Subscription;
  user?: UserModel;
  orderId?: string;

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private orderService: OrderServiceService,
    private router: Router) {
  }

  ngOnInit(): void {
    // Get authenticated user
    this.user = this.authService.getUser();

    // Get the order ID from the route params
    this.paramsSubscription = this.route.params.subscribe(params => {
      this.orderId = params['id'];
      
      if (this.orderId) {
        // Fetch the specific order by ID
        this.order$ = this.orderService.getOrderById(this.orderId);

        this.orderSubscription = this.order$.subscribe({
          next: (response) => {
            this.order = response;
          }
        });
      }
    });
  }

  calculateOrderTotal(): number {
    if (this.order) {
      return this.order.totalAmount;
    }
    return 0;
  }

  backToOrders(): void {
    this.router.navigateByUrl('/orders');
  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.orderSubscription?.unsubscribe();
  }
}
