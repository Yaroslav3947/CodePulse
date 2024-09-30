import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { Order } from '../../blog-post/models/order.model';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from '../services/order.service';
import { UpdateOrderRequest } from '../../blog-post/models/update-order-request.model';

@Component({
  selector: 'app-edit-order',
  // standalone: true,
  // imports: [],
  templateUrl: './edit-order.component.html',
  styleUrl: './edit-order.component.css'
})
export class EditOrderComponent {

  id: string | null = null;

  paramsSubscription?: Subscription;
  editOrderSubscription?: Subscription;
  order?: Order;

    constructor(private route: ActivatedRoute,
      private orderService: OrderService,
      private router: Router) {
      
    }

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if(this.id) {
          // get data from API by id
          this.orderService.getOrderById(this.id)
          .subscribe({
            next: (response) => {
            this.order = response;
            }
          });
        }
      }
    });
  }

  onFormSubmit(): void {
    const updateOrderRequest: UpdateOrderRequest = {
      status: this.order?.status ?? ' ',
    };

    // pass this object to service
    if(this.id) {
      this.orderService.updateOrder(this.id, updateOrderRequest)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/orders')
        }
      });
    }
  }

  onDelete(): void {
    if(this.id) {
      this.orderService.deleteOrder(this.id)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/orders')
        }
      });
    }
  }

  ngOnDestroy(): void {
      this.paramsSubscription?.unsubscribe;
      this.editOrderSubscription?.unsubscribe;
  }
}
