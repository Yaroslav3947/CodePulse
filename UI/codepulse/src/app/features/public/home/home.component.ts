import { Component, OnInit } from '@angular/core';
import { CosmeticService } from '../../blog-post/services/cosmetic.service';
import { Observable } from 'rxjs';
import { Cosmetic } from '../../blog-post/models/cosmetic.model';

@Component({
  selector: 'app-home',
  // standalone: true,
  // imports: [],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})


export class HomeComponent implements OnInit {

  cosmetics$?: Observable<Cosmetic[]>;
  constructor(private cosmeticService: CosmeticService) {

  }

  ngOnInit():void {
     this.cosmetics$ = this.cosmeticService.getAllCosmetics();
  }


}