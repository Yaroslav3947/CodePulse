import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CosmeticService } from '../../blog-post/services/cosmetic.service';
import { Observable, Subscription } from 'rxjs';
import { AuthService } from '../../auth/services/auth.service';
import { UserModel } from '../../auth/models/user.model';
import { BasketLike as BasketLike } from '../models/add-like.model';
import { BasketService } from '../services/basket.service';
import { Cosmetic } from '../../blog-post/models/cosmetic.model';
import { UsersService } from '../../users/services/users.service';

@Component({
  selector: 'app-cosmetic-details',
  // standalone: true,
  // imports: [],
  templateUrl: './cosmetic-details.component.html',
  styleUrls: ['./cosmetic-details.component.css'],
})
export class CosmeticDetailsComponent implements OnInit, OnDestroy {

  url: string | null = null;
  cosmetic$?: Observable<Cosmetic>;

  cosmetic?: Cosmetic;
  user?: UserModel;
  isLikedByUser: boolean = false;
  commentDescription?: string;

  isHovering = false;

  addLikeSubscription?: Subscription;
  getCosmeticSubscription?: Subscription;
  addCommentSubscription?: Subscription;
  removeLikeSubscription?: Subscription;
  getCosmeticsInBaskerSubscription?: Subscription;

    constructor(private route: ActivatedRoute,
      private cosmeticService: CosmeticService,
      private authService: AuthService,
      private router: Router,
      private basketService: BasketService,
      private usersService: UsersService) {

    }
  ngOnDestroy(): void {
    this.addLikeSubscription?.unsubscribe();
    this.getCosmeticSubscription?.unsubscribe();
    this.addCommentSubscription?.unsubscribe();
    this.removeLikeSubscription?.unsubscribe();
    this.getCosmeticsInBaskerSubscription?.unsubscribe();
  }

  ngOnInit(): void {

    this.route.paramMap.subscribe({
      next: (params) => {
        this.url = params.get('url');
        

        if (this.url) {
          this.cosmetic$ = this.cosmeticService.getCosmeticByUrlHandle(this.url);
  
          // Get Cosmetic ID
          this.getCosmeticSubscription = this.cosmeticService.getCosmeticByUrlHandle(this.url).subscribe({
            next: (response) => {
              this.cosmetic = response;
              

              if (this.user && this.cosmetic) {
                this.getCosmeticsInBaskerSubscription = this.usersService.getCosmeticsIDInBasket(this.user.userId).subscribe({
                  next: (response) => {
                    this.isLikedByUser = response.includes(this.cosmetic!.id);
                  }
                });
              }
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
    if (this.user && this.cosmetic) {
      const likeCosmeticRequest: BasketLike = {
        userId: this.user.userId,
        cosmeticId: this.cosmetic.id
      };

    if(!this.isLikedByUser) {
      this.addLikeSubscription = this.basketService.addToBasket(likeCosmeticRequest)
        .subscribe({
          next: (response) => {
              this.isLikedByUser = true;
              // TODO: fix so no reload is needed to change like button and totalLikes and forbid click again
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