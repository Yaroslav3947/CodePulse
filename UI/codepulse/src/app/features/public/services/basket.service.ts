import { Injectable } from '@angular/core';
import { BasketLike } from '../models/add-like.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class BasketService {

  constructor(private http: HttpClient) { }

  addToBasket(basketLike: BasketLike): Observable<BasketLike> {
    return this.http.post<BasketLike>(`/api/basket/add`, basketLike);
  }

  removeFromBasket(basketLike: BasketLike): Observable<BasketLike> {
    return this.http.post<BasketLike>(`/api/basket/remove`, basketLike);
  }
}
