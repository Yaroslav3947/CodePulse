import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { User } from '../models/user.model';
import { Observable } from 'rxjs';
import { UpdateUserRequest } from '../models/update-user-request';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient,
    private cookieService: CookieService) { }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`/api/users?addAuth=true`);
  }
  
  getUserById(id: string): Observable<User> {
    return this.http.get<User>(`/api/users/${id}?addAuth=true`)
  }

  updateUser(id: string, updateUserRequest: UpdateUserRequest): Observable<User> {
    return this.http.put<User>(`/api/users/${id}?addAuth=true`, 
    updateUserRequest);
  }

  deleteUser(id: string): Observable<User> {
    return this.http.delete<User>(`/api/users/${id}?addAuth=true`);
  }
}
