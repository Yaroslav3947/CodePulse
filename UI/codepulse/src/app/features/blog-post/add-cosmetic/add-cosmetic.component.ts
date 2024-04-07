import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddCosmetic } from '../models/add-cosmetic.model';
import { CosmeticService } from '../services/cosmetic.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable, Subscription } from 'rxjs'
import { Category } from '../../category/models/category.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-cosmetic',
  templateUrl: './add-cosmetic.component.html',
  styleUrls: ['./add-cosmetic.component.css']
})
export class AddCosmeticComponent implements OnInit, OnDestroy {
  model: AddCosmetic;
  categories$?: Observable<Category[]>;

  isImageSelectorVisible: boolean = false;

  imageSelectorSubscription?: Subscription;


  constructor(private cosmeticService: CosmeticService,
    private router: Router,
    private categoryService: CategoryService,
    private imageService: ImageService) {
    this.model = {
      name: '',
      description: '',
      brand: '',
      featuredImageUrl: '',
      urlHandle: '',
      price: 0,
      publishedDate: new Date(),
      categories: []
    }
  }

  onFormSubmit(): void {
    console.log(this.model);
    this.cosmeticService.createCosmetic(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/cosmetics');
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
