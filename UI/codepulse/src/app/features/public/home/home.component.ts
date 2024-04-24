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

    this.categoryService.getAllCategories().subscribe({
      next: (response) => {
        this.categories = response.map(category => ({ ...category, checked: true })); 
        this.selectedCategories = [...this.categories];
        this.filterCosmetics(); 
      }
    });
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


  filterCosmetics(): void {
    if (this.selectedCategories.length === 0) {
      this.filteredCosmetics = this.cosmetics;
    } else {
      this.filteredCosmetics = this.cosmetics.filter(cosmetic =>
        this.selectedCategories.some(category => cosmetic.categories.some(c => c.id === category.id))
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
    this.filterCosmetics(); 
  }
  

}