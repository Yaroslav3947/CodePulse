import { Injectable } from '@angular/core';
import { AddCosmetic } from '../models/add-cosmetic.model';
import { Observable } from 'rxjs';
import { Cosmetic } from '../models/cosmetic.model';
import { HttpClient } from '@angular/common/http';
import { UpdateCosmetic } from '../models/update-cosmetic.model';

@Injectable({
  providedIn: 'root'
})
export class CosmeticService {

  constructor(private http: HttpClient) { }

  
  getAllCosmetics(): Observable<Cosmetic[]> {
    return this.http.get<Cosmetic[]>(`/api/cosmetics`);
  }
  
  getCosmeticById(id: string): Observable<Cosmetic> {
    return this.http.get<Cosmetic>(`/api/cosmetics/${id}`);
  }
  
  getCosmeticByUrlHandle(urlHandle: string): Observable<Cosmetic> {
    return this.http.get<Cosmetic>(`/api/cosmetics/${urlHandle}`);
  }
  
  createCosmetic(data: AddCosmetic): Observable<Cosmetic> {
    return this.http.post<Cosmetic>(`/api/cosmetics?addAuth=true`, data);
  }

  updateCosmetic(id: string, updatedBlogPost: UpdateCosmetic): Observable<Cosmetic> {
    return this.http.put<Cosmetic>(`/api/cosmetics/${id}?addAuth=true`, updatedBlogPost);
  }

  deleteCosmetic(id: string): Observable<Cosmetic> {
    return this.http.delete<Cosmetic>(`/api/cosmetics/${id}?addAuth=true`)
  }
}
