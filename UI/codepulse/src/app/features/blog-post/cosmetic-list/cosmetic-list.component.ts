import { Component, OnInit } from '@angular/core';
import { CosmeticService } from '../services/cosmetic.service';
import { Observable } from 'rxjs';
import { Cosmetic } from '../models/cosmetic.model';

@Component({
  selector: 'app-cosmetic-list',
  templateUrl: './cosmetic-list.component.html',
  styleUrls: ['./cosmetic-list.component.css']
})
export class CosmeticListComponent implements OnInit {

  cosmetic$?: Observable<Cosmetic[]>;
  
constructor(private cosmeticService: CosmeticService) {

} 

  ngOnInit(): void {
    this.cosmetic$ = this.cosmeticService.getAllCosmetics();
  }
}
