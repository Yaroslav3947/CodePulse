import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddProduct } from '../models/add-product.model';
import { ProductService } from '../services/product.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable, Subscription } from 'rxjs'
import { Category } from '../../category/models/category.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit, OnDestroy {
  model: AddProduct;
  categories$?: Observable<Category[]>;

  isImageSelectorVisible: boolean = false;

  imageSelectorSubscription?: Subscription;


  constructor(private productService: ProductService,
    private router: Router,
    private categoryService: CategoryService,
    private imageService: ImageService) {
    this.model = {
      name: '',
      description: '',
      stock: 0,
      featuredImageUrl: '',
      urlHandle: '',
      price: 0,
      publishedDate: new Date(),
      categories: []
    }
  }

  onFormSubmit(): void {
    console.log(this.model);
    this.productService.createProduct(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/products');
        }
      });
  }


  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();

    this.imageSelectorSubscription = this.imageService.onSelectImage()
      .subscribe({
        next: (response) => {
          if (this.model) {
            this.model.featuredImageUrl = response.url;
            this.isImageSelectorVisible = false;
          }
        }
      })
  }

  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector(): void {
    this.isImageSelectorVisible = false;
  }

  ngOnDestroy(): void {
    this.imageSelectorSubscription?.unsubscribe();
  }


}
