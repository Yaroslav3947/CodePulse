import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../blog-post/services/product.service';
import { Observable } from 'rxjs';
import { Product } from '../../blog-post/models/product.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})


export class HomeComponent implements OnInit {

  products$?: Observable<Product[]>;
  filteredProducts: Product[] = [];

  products: Product[] = [];
  searchStock: string = '';

  categories: Category[] = [];
  selectedCategories: Category[] = [];
  
  constructor(private productService: ProductService,
    private categoryService: CategoryService) {
  }

  ngOnInit():void {
     this.products$ = this.productService.getAllProducts();
     this.productService.getAllProducts().subscribe(
      products => {
      this.products = products;
      this.filteredProducts = products;
    });

    this.categoryService.getAllCategories().subscribe({
      next: (response) => {
        this.categories = response.map(category => ({ ...category, checked: true })); 
        this.selectedCategories = [...this.categories];
        this.filterproducts(); 
      }
    });
  }

  filterByStock() {
    console.log(this.filteredProducts.length)
    if (this.searchStock.trim() === '') {
      this.filteredProducts = this.products;
    } else {
      // this.filteredProducts = this.products.filter(product =>
        // product.stock.includes(this.searchStock)
      // );
    }
  }


  filterproducts(): void {
    if (this.selectedCategories.length === 0) {
      this.filteredProducts = this.products;
    } else {
      this.filteredProducts = this.products.filter(product =>
        this.selectedCategories.some(category => product.categories.some(c => c.id === category.id))
      ); 
    }
  }

  onCheckboxChange(category: Category): void {
    const index = this.selectedCategories.findIndex(c => c.id === category.id);
    if (index !== -1) {
      this.selectedCategories.splice(index, 1); 
    } else {
      this.selectedCategories.push(category); 
    }
    this.filterproducts(); 
  }
  

}