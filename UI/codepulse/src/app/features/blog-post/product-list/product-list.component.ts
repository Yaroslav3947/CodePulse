import { Component, OnInit } from '@angular/core';
import { ProductService as ProductService } from '../services/product.service';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  product$?: Observable<Product[]>;
  
constructor(private productService: ProductService) {

} 

  ngOnInit(): void {
    this.product$ = this.productService.getAllProducts();
  }
}
