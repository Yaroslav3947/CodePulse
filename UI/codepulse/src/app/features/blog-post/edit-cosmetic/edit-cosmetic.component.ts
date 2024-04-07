import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { CosmeticService } from '../services/cosmetic.service';
import { Cosmetic } from '../models/cosmetic.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { UpdateCosmetic } from '../models/update-cosmetic.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-edit-cosmetic',
  templateUrl: './edit-cosmetic.component.html',
  styleUrls: ['./edit-cosmetic.component.css']
})
export class EditCosmeticComponent implements OnInit, OnDestroy {
  id: string | null = null;
  cosmetic?: Cosmetic;
  categories$?: Observable<Category[]>;
  selectedCategories?: string[];

  isImageSelectorVisible: boolean = false;

  routeSubscription?: Subscription;
  updateCosmeticSubscription?: Subscription;
  getCosmeticSubscription?: Subscription;
  deleteCosmeticSubscription?: Subscription;
  imageSelectSubscription?: Subscription;

  constructor(private route: ActivatedRoute,
    private cosmeticService: CosmeticService,
    private categoryService: CategoryService,
    private router: Router,
    private imageService: ImageService) {
  }

  ngOnInit(): void {

    this.categories$ = this.categoryService.getAllCategories();

    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        // Get Cosmetics from API by id

        if (this.id) {
          this.getCosmeticSubscription = this.cosmeticService.getCosmeticById(this.id)
            .subscribe({
              next: (response) => {
                this.cosmetic = response;
                this.selectedCategories = response.categories.map(x => x.id);
              }
            });
        }

        this.imageSelectSubscription = this.imageService.onSelectImage()
          .subscribe({
            next: (response) => {
              if (this.cosmetic) {
                this.cosmetic.featuredImageUrl = response.url;
                this.isImageSelectorVisible = false;
              }
            }
          })
      }
    });
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateCosmeticSubscription?.unsubscribe();
    this.getCosmeticSubscription?.unsubscribe();
    this.deleteCosmeticSubscription?.unsubscribe();
    this.imageSelectSubscription?.unsubscribe();
  }

  onFormSubmit(): void {
    // Convert model to Request Object
    if (this.cosmetic && this.id) {
      const updateCosmetic: UpdateCosmetic = {
        name: this.cosmetic.name,
        description: this.cosmetic.description,
        price: this.cosmetic.price,
        featuredImageUrl: this.cosmetic.featuredImageUrl,
        urlHandle: this.cosmetic.urlHandle,
        brand: this.cosmetic.brand,
        publishedDate: this.cosmetic.publishedDate,
        categories: this.selectedCategories ?? []
      };

      this.updateCosmeticSubscription = this.cosmeticService
        .updateCosmetic(this.id, updateCosmetic)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('admin/cosmetics');
          }
        })
    }
  }

  onDelete(): void {
    if (this.id) {
      this.deleteCosmeticSubscription = this.cosmeticService.deleteCosmetic(this.id)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/cosmetics')
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
