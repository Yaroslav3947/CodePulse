import { Injectable } from '@angular/core';
import { AddProduct } from '../models/add-product.model';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { HttpClient } from '@angular/common/http';
import { UpdateProduct } from '../models/update-product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  
  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`/api/products`);
  }
  
  getProductById(id: string): Observable<Product> {
    return this.http.get<Product>(`/api/products/${id}`);
  }
  
  getProductByUrlHandle(urlHandle: string): Observable<Product> {
    return this.http.get<Product>(`/api/products/${urlHandle}`);
  }
  
  createProduct(data: AddProduct): Observable<Product> {
    return this.http.post<Product>(`/api/products?addAuth=true`, data);
  }

  updateProduct(id: string, updateProduct: UpdateProduct): Observable<Product> {
    return this.http.put<Product>(`/api/products/${id}?addAuth=true`, updateProduct);
  }

  deleteProduct(id: string): Observable<Product> {
    return this.http.delete<Product>(`/api/products/${id}?addAuth=true`)
  }
}
