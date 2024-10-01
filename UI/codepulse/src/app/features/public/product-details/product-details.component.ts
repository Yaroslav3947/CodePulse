import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../blog-post/services/product.service';
import { Observable, Subscription } from 'rxjs';
import { AuthService } from '../../auth/services/auth.service';
import { UserModel } from '../../auth/models/user.model';
import { BasketLike as BasketLike } from '../models/add-like.model';
import { BasketService } from '../services/basket.service';
import { Product } from '../../blog-post/models/product.model';
import { UsersService } from '../../users/services/users.service';
import { OrderService } from '../../order/services/order.service';

@Component({
  selector: 'app-product-details',
  // standalone: true,
  // imports: [],
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css'],
})
export class ProductDetailsComponent implements OnInit, OnDestroy {

  url: string | null = null;
  product$?: Observable<Product>;

  product?: Product;
  user?: UserModel;
  isLikedByUser: boolean = false;
  commentDescription?: string;
  orderId?: string;

  isHovering = false;

  addLikeSubscription?: Subscription;
  getProductSubscription?: Subscription;
  addCommentSubscription?: Subscription;
  removeLikeSubscription?: Subscription;
  getProductsInBasketSubscription?: Subscription;

    constructor(private route: ActivatedRoute,
      private productService: ProductService,
      private authService: AuthService,
      private router: Router,
      private basketService: BasketService,
      private usersService: UsersService,
      private orderService: OrderService) {

    }
  ngOnDestroy(): void {
    this.addLikeSubscription?.unsubscribe();
    this.getProductSubscription?.unsubscribe();
    this.addCommentSubscription?.unsubscribe();
    this.removeLikeSubscription?.unsubscribe();
    this.getProductsInBasketSubscription?.unsubscribe();
  }

  ngOnInit(): void {

    this.route.paramMap.subscribe({
      next: (params) => {
        this.url = params.get('url');
        

        if (this.url) {
          this.product$ = this.productService.getProductByUrlHandle(this.url);
  
          this.getProductSubscription = this.productService.getProductByUrlHandle(this.url).subscribe({
            next: (response) => {
              this.product = response;
              

              if (this.user && this.product) {
                this.getProductsInBasketSubscription = this.usersService.getProductsIDInBasket(this.user.userId).subscribe({
                  next: (response) => {
                    this.isLikedByUser = response.includes(this.product!.id);
                  }
                });
              }

            this.orderService.getOrderByUserId(this.user!.userId).subscribe({
              next:(response) => {
                this.orderId = response.id;
              }
            })
            }
          });
        }
      }
    });
  

    this.authService.user().subscribe({
      next: (response) => {
        this.user = response;
      }
    });
  

    this.user = this.authService.getUser();
  }

  
  makePriceBigger(): void {
    this.isHovering = true;
  }

  resetPriceSize(): void {
    this.isHovering = false;
  }

  IsUserLoggedIn():boolean {
    return this.user !== undefined;
  }

  likeButtonClick(): void {
    if (this.user && this.product) {
      const likeCosmeticRequest: BasketLike = {
        userId: this.user.userId,
        productId: this.product.id
      };

      console.log(likeCosmeticRequest.productId);

    if(!this.isLikedByUser) {
      this.addLikeSubscription = this.basketService.addToBasket(likeCosmeticRequest)
        .subscribe({
          next: (response) => {
              this.isLikedByUser = true;
              // TODO: fix so no reload is needed to change like button and totalLikes and forbid click again
              this.orderService.addProductToOrder(this.orderId!, this.product!.id, 1);
            }
          }
        )
    } else {
        this.removeLikeSubscription = this.basketService.removeFromBasket(likeCosmeticRequest)
        .subscribe({
          next: (response) => {
              this.isLikedByUser = false;
            }
          }
        )
      }
    }
  }

}