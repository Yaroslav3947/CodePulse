import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { UsersService } from 'src/app/features/users/services/users.service';
import { Cosmetic } from '../../models/cosmetic.model';
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

  cosmetics$?: Observable<Cosmetic[]>;

  cosmetics?: Cosmetic[];

  paramsSubscription?: Subscription;
  cosmeticsSubscription?: Subscription
  user?: UserModel;

    constructor(private route: ActivatedRoute,
      private usersService: UsersService,
      private authService: AuthService,
      private basketService: BasketService,
      private router: Router) {
      
    }

  ngOnInit(): void {
    
    this.user = this.authService.getUser();

    console.log(this.user)

    this.cosmetics$ = this.usersService.getCosmeticsInBasket(this.user!.userId);

    this.cosmeticsSubscription = this.usersService.getCosmeticsInBasket(this.user!.userId)
      .subscribe({
        next: (response) => {
          this.cosmetics = response;
        }
      });
  }

  calculateTotalPrice(): number {
    if(this.cosmetics) {
      return this.cosmetics.reduce((total, current) => total + current.price, 0);
    }

    return 0;
  }

  removeCosmetic(cosmetic: Cosmetic): void {
    if(this.user && this.cosmetics) {

    
    const basketLike: BasketLike = {
      userId: this.user.userId ?? ' ',
      cosmeticId: cosmetic.id ?? ' '
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
