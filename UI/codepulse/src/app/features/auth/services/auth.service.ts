import { Injectable } from '@angular/core';
import { LoginRequest } from '../models/login-request.models';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { HttpClient } from '@angular/common/http';
import { UserModel } from '../models/user.model';
import { CookieService } from 'ngx-cookie-service';
import { RegisterRequest } from '../models/register-request.model';
import { RegisterResponse } from '../models/register-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  $user = new BehaviorSubject<UserModel | undefined>(undefined)

  constructor(private http: HttpClient,
    private cookieService: CookieService) { }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`/api/auth/login`,{
      email: request.email,
      password: request.password,
    });
  }

  register(request: RegisterRequest): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(`/api/auth/register`,{
      email: request.email,
      password: request.password
    });
  }

  getUser(): UserModel | undefined {
    const email = localStorage.getItem('user-email');
    const roles = localStorage.getItem('user-roles');
    const userId = localStorage.getItem('user-id');

    if(email && roles && userId) {
      const user: UserModel = {
        email: email,
        roles: roles.split(','),
        userId: userId
      };

      return user;
    }

    return undefined;
  }

  setUser(user: UserModel): void {
    this.$user.next(user);
    localStorage.setItem('user-email', user.email);
    localStorage.setItem('user-roles', user.roles.join(','));
    localStorage.setItem('user-id', user.userId);
  }

  user(): Observable<UserModel | undefined> {
    return this.$user.asObservable();
  }

  logout(): void {
    localStorage.clear();
    this.cookieService.delete('Authorization', '/');
    this.$user.next(undefined); // User log out
  }
}
