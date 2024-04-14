import { Component, OnInit } from '@angular/core';
import { CosmeticService } from '../../blog-post/services/cosmetic.service';
import { Observable } from 'rxjs';
import { Cosmetic } from '../../blog-post/models/cosmetic.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})


export class HomeComponent implements OnInit {

  cosmetics$?: Observable<Cosmetic[]>;
  filteredCosmetics: Cosmetic[] = [];

  cosmetics: Cosmetic[] = [];
  searchBrand: string = '';

  categories: Category[] = [];
  selectedCategories: Category[] = [];
  
  constructor(private cosmeticService: CosmeticService,
    private categoryService: CategoryService) {
  }

  ngOnInit():void {
     this.cosmetics$ = this.cosmeticService.getAllCosmetics();
     this.cosmeticService.getAllCosmetics().subscribe(
      cosmetics => {
      this.cosmetics = cosmetics;
      this.filteredCosmetics = cosmetics;
    });

    this.categoryService.getAllCategories().subscribe(
      categories => {
        this.categories = categories;
        this.selectedCategories = categories;
      }
    )
    
  }

  filterByBrand() {
    console.log(this.filteredCosmetics.length)
    if (this.searchBrand.trim() === '') {
      this.filteredCosmetics = this.cosmetics;
    } else {
      this.filteredCosmetics = this.cosmetics.filter(cosmetic =>
        cosmetic.brand.toLowerCase().includes(this.searchBrand.toLowerCase())
      );
    }
  }

  onCategoryChange(category: Category) {
    if (category) {
      this.selectedCategories.push(category);
    } else {
      this.selectedCategories = this.selectedCategories.filter(c => c !== category);
    }
    this.filterCosmetics();
  }

  filterCosmetics() {
    if (this.selectedCategories.length === 0) {
      this.filteredCosmetics = this.cosmetics;
    } else {
      this.filteredCosmetics = this.cosmetics.filter(cosmetic =>
        this.selectedCategories.some(categoryId => cosmetic.categories.includes(categoryId))
      );
    }
  }

}