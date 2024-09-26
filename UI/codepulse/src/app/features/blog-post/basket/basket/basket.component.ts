import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { UsersService } from 'src/app/features/users/services/users.service';
import { Product } from '../../models/product.model';
import { AuthService } from 'src/app/features/auth/services/auth.service';
import { UserModel } from 'src/app/features/auth/models/user.model';
import { BasketService } from 'src/app/features/public/services/basket.service';
import { BasketLike } from 'src/app/features/public/models/add-like.model';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.css'
})
export class BasketComponent implements OnInit, OnDestroy {

  products$?: Observable<Product[]>;

  products?: Product[];

  paramsSubscription?: Subscription;
  productsSubscription?: Subscription
  user?: UserModel;

    constructor(private route: ActivatedRoute,
      private usersService: UsersService,
      private authService: AuthService,
      private basketService: BasketService,
      private router: Router) {
      
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
  }

  calculateTotalPrice(): number {
    if(this.products) {
      return this.products.reduce((total, current) => total + current.price, 0);
    }

    return 0;
  }

  removeProduct(product: Product): void {
    if(this.user && this.products) {

    
    const basketLike: BasketLike = {
      userId: this.user.userId ?? ' ',
      productId: product.id ?? ' '
    }
    this.basketService.removeFromBasket(basketLike)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/basket')
        }
      });

    }
  }

  buy(): void {

  }

  ngOnDestroy(): void {
      this.paramsSubscription?.unsubscribe;
  }
}
