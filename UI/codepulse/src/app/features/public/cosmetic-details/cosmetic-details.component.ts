import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CosmeticService } from '../../blog-post/services/cosmetic.service';
import { Observable, Subscription } from 'rxjs';
import { Cosmetic } from '../../blog-post/models/cosmetic.model';
import { AuthService } from '../../auth/services/auth.service';
import { UserModel } from '../../auth/models/user.model';

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

  getCosmeticSubscription?: Subscription;
  addCosmeticSubscription?: Subscription;

    constructor(private route: ActivatedRoute,
      private cosmeticService: CosmeticService,
      private authService: AuthService,
      private router: Router) {

    }
  ngOnDestroy(): void {
    this.getCosmeticSubscription?.unsubscribe();
    this.addCosmeticSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.route.paramMap
    .subscribe({
      next: (params) => {
        this.url = params.get('url')
      }
    });


    // Get to know if user is registered
    this.authService.user()
   .subscribe({
      next: (response) => {
        this.user = response;
      }
   });

    // Fetch blog details by url
    if(this.url) {
      this.cosmetic$ = this.cosmeticService.getCosmeticByUrlHandle(this.url)

      // Get to know blogPostId
      this.getCosmeticSubscription = this.cosmeticService
        .getCosmeticByUrlHandle(this.url)
        .subscribe({
          next: (response) => {
            this.cosmetic = response;
          }
        })
    }

   this.user = this.authService.getUser();
  }

  IsUserLoggedIn():boolean {
    return this.user !== undefined;
  }


  onFormSubmit(): void {
  }

}
