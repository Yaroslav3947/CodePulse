import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blog-post.model';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit, OnDestroy {
  id: string | null = null;
  routeSubscription?: Subscription;
  blogPost?: BlogPost;

  constructor(private route: ActivatedRoute,
    private blogpostService: BlogPostService,
      private router: Router) {

  }

  ngOnInit(): void {
      this.routeSubscription = this.route.paramMap.subscribe({
        next: (params) => {
          this.id = params.get('id');

          // Get BlogPost from API by id

          if(this.id) {
            this.blogpostService.getBlogPostById(this.id)
            .subscribe({
              next: (response) => {
              this.blogPost = response;
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
