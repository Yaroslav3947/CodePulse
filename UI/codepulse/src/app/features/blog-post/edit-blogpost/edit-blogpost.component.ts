import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blog-post.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { UpdateBlogPost } from '../models/update-blog-post.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit, OnDestroy {
  id: string | null = null;
  blogPost?: BlogPost;
  categories$?: Observable<Category[]>;
  selectedCategories?: string[];

  isImageSelectorVisible: boolean = false;

  routeSubscription?: Subscription;
  updateBlogPostSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;
  deleteBlogPostSubscription?: Subscription;
  imageSelectSubscription?: Subscription;

  constructor(private route: ActivatedRoute,
    private blogpostService: BlogPostService,
    private categoryService: CategoryService,
    private router: Router,
    private imageService: ImageService) {
  }

  ngOnInit(): void {

    this.categories$ = this.categoryService.getAllCategories();

    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        // Get BlogPost from API by id

        if (this.id) {
          this.getBlogPostSubscription = this.blogpostService.getBlogPostById(this.id)
            .subscribe({
              next: (response) => {
                this.blogPost = response;
                this.selectedCategories = response.categories.map(x => x.id);
              }
            });
        }

        this.imageSelectSubscription = this.imageService.onSelectImage()
          .subscribe({
            next: (response) => {
              if (this.blogPost) {
                this.blogPost.featuredImageUrl = response.url;
                this.isImageSelectorVisible = false;
              }
            }
          })
      }
    });
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateBlogPostSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
    this.deleteBlogPostSubscription?.unsubscribe();
    this.imageSelectSubscription?.unsubscribe();
  }

  onFormSubmit(): void {
    // Conver model to Request Object
    if (this.blogPost && this.id) {
      const updateBlogPost: UpdateBlogPost = {
        title: this.blogPost.title,
        shortDescription: this.blogPost.shortDescription,
        content: this.blogPost.content,
        featuredImageUrl: this.blogPost.featuredImageUrl,
        urlHandle: this.blogPost.urlHandle,
        author: this.blogPost.author,
        publishedDate: this.blogPost.publishedDate,
        isVisible: this.blogPost.isVisible,
        categories: this.selectedCategories ?? []
      };

      this.updateBlogPostSubscription = this.blogpostService
        .updateBlogPost(this.id, updateBlogPost)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('admin/blogposts');
          }
        })
    }
  }

  onDelete(): void {
    if (this.id) {
      this.deleteBlogPostSubscription = this.blogpostService.deleteBlogPost(this.id)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/blogposts')
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
