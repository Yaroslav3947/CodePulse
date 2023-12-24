import { inject } from '@angular/core';
import { CanActivateFn, Router, UrlTree } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../services/auth.service';
import { jwtDecode } from "jwt-decode";

interface DecodedToken {
  foo: string;
  exp: number;
  iat: number;
}
export const authGuard: CanActivateFn = (route, state): boolean | UrlTree => {

  const cookieService = inject(CookieService);
  const authService = inject(AuthService);
  const router = inject(Router);
  const user = authService.getUser();

  let token = cookieService.get('Authorization');

  if (token && user) {
    token = token.replace('Bearer ', '');

    const decodedToken: DecodedToken = jwtDecode(token);

    const expirationDate = decodedToken.exp * 1000;
    const currentTime = new Date().getTime();

    if (expirationDate < currentTime) {
      // Logout
      authService.logout();
      return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } })
    } else {
      // Token is still valid
      if (user?.roles.includes('Writer')) {
        return true;
      } else {
        alert('Unathorized');
        return false;
      }
    }
  } else {
    // Logout
    authService.logout();
    return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } });
  }

};
