import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blog-post.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit, OnDestroy {
  id: string | null = null;
  routeSubscription?: Subscription;
  blogPost?: BlogPost;
  categories$?: Observable<Category[]>;
  selectedCategories?: string[];

  constructor(private route: ActivatedRoute,
    private blogpostService: BlogPostService,
    private categoryService: CategoryService,
      private router: Router) {
  }

  ngOnInit(): void {

      this.categories$ = this.categoryService.getAllCategories();
      
      this.routeSubscription = this.route.paramMap.subscribe({
        next: (params) => {
          this.id = params.get('id');

          // Get BlogPost from API by id

          if(this.id) {
            this.blogpostService.getBlogPostById(this.id)
            .subscribe({
              next: (response) => {
              this.blogPost = response;
              this.selectedCategories = response.categories.map(x => x.id);
              }
            });
          }
        }
      });
  }

  ngOnDestroy(): void {
      this.routeSubscription?.unsubscribe();
  }

  onFormSubmit(): void {

  }
  
}
