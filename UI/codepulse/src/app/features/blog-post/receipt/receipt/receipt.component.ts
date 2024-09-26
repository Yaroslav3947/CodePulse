import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Product } from '../../models/product.model';
import { UserModel } from 'src/app/features/auth/models/user.model';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/features/auth/services/auth.service';
import { UsersService } from 'src/app/features/users/services/users.service';

@Component({
  selector: 'app-receipt',
  templateUrl: './receipt.component.html',
  styleUrl: './receipt.component.css'
})
export class ReceiptComponent implements OnInit, OnDestroy {

  products?: Product[];
  products$?: Observable<Product[]>;

  productsSubscription?: Subscription

  user?: UserModel;

  invoiceNumber: number | undefined;
  currentDate: Date | undefined;

    constructor(private route: ActivatedRoute,
      private usersService: UsersService,
      private authService: AuthService,
      private router: Router) {
      
    }
  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }
  ngOnInit(): void {
    this.user = this.authService.getUser();
  
    this.products$ = this.usersService.getProductsInBasket(this.user!.userId);

    this.productsSubscription = this.usersService.getProductsInBasket(this.user!.userId)
      .subscribe({
        next: (response) => {
          this.products = response;
        }
      });

    this.invoiceNumber = Math.floor(Math.random() * 100000) + 1;
    this.createInvoiceNumberElement();

    this.currentDate = new Date();
    this.createCurrentDateElement();
  }

  calculateTotalPrice(): number {
    if(this.products) {
      return this.products.reduce((total, current) => total + current.price, 0);
    }

    return 0;
  }

  private createInvoiceNumberElement(): void {
    const invoiceNumberElement = document.createElement('span');
    invoiceNumberElement.innerText = this.invoiceNumber?.toString() || '';
    const targetElement = document.getElementById('invoiceNumber');
    if (targetElement) {
      targetElement.appendChild(invoiceNumberElement);
    }
  }

  private createCurrentDateElement(): void {
    const currentDateElement = document.createElement('span');
    currentDateElement.innerText = this.currentDate?.toLocaleDateString() || '';
    const targetElement = document.getElementById('currentDate');
    if (targetElement) {
      targetElement.appendChild(currentDateElement);
    }
  }

}
