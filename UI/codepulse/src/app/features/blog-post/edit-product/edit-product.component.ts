import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { ProductService as ProductService } from '../services/product.service';
import { Product } from '../models/product.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { UpdateProduct } from '../models/update-product.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit, OnDestroy {
  id: string | null = null;
  product?: Product;
  categories$?: Observable<Category[]>;
  selectedCategories?: string[];

  isImageSelectorVisible: boolean = false;

  routeSubscription?: Subscription;
  updateProductSubscription?: Subscription;
  getProductSubscription?: Subscription;
  deleteProductSubscription?: Subscription;
  imageSelectSubscription?: Subscription;

  constructor(private route: ActivatedRoute,
    private productService: ProductService,
    private categoryService: CategoryService,
    private router: Router,
    private imageService: ImageService) {
  }

  ngOnInit(): void {

    this.categories$ = this.categoryService.getAllCategories();

    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        // Get Products from API by id

        if (this.id) {
          this.getProductSubscription = this.productService.getProductById(this.id)
            .subscribe({
              next: (response) => {
                this.product = response;
                this.selectedCategories = response.categories.map(x => x.id);
              }
            });
        }

        this.imageSelectSubscription = this.imageService.onSelectImage()
          .subscribe({
            next: (response) => {
              if (this.product) {
                this.product.featuredImageUrl = response.url;
                this.isImageSelectorVisible = false;
              }
            }
          })
      }
    });
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateProductSubscription?.unsubscribe();
    this.getProductSubscription?.unsubscribe();
    this.deleteProductSubscription?.unsubscribe();
    this.imageSelectSubscription?.unsubscribe();
  }

  onFormSubmit(): void {
    // Convert model to Request Object
    if (this.product && this.id) {
      const updateProduct: UpdateProduct = {
        name: this.product.name,
        description: this.product.description,
        price: this.product.price,
        featuredImageUrl: this.product.featuredImageUrl,
        urlHandle: this.product.urlHandle,
        stock: this.product.stock,
        publishedDate: this.product.publishedDate,
        categories: this.selectedCategories ?? []
      };

      this.updateProductSubscription = this.productService
        .updateProduct(this.id, updateProduct)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('admin/products');
          }
        })
    }
  }

  onDelete(): void {
    if (this.id) {
      this.deleteProductSubscription = this.productService.deleteProduct(this.id)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/products')
          }
        });
    }
  }

  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }
  closeImageSelector(): void {
    this.isImageSelectorVisible = false;
  }

}
