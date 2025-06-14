import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from 'src/app/features/auth/models/user.model';
import { AuthService } from 'src/app/features/auth/services/auth.service';
import { ThemeService } from 'src/app/services/theme.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  
  user?: UserModel;
  isDark$: Observable<boolean>;

  constructor(private authService: AuthService,
    private router: Router,
    private themeService: ThemeService) {
    this.isDark$ = this.themeService.isDarkMode$;
  }
  
  ngOnInit(): void {
   this.authService.user()
   .subscribe({
      next: (response) => {
        this.user = response;
      }
   });

   this.user = this.authService.getUser();
  }
  
  onLogout(): void {
   this.authService.logout();
   this.router.navigateByUrl('/');
  }

}
