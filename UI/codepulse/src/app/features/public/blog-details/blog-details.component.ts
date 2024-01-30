import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { Observable, Subscription } from 'rxjs';
import { BlogPost } from '../../blog-post/models/blog-post.model';
import { AuthService } from '../../auth/services/auth.service';
import { UserModel } from '../../auth/models/user.model';
import { AddLikeRequest } from '../models/add-like.model';
import { AddLikeCommentsServiceService } from '../services/add-like-comments.service.service';

@Component({
  selector: 'app-blog-details',
  // standalone: true,
  // imports: [],
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.css']
})
export class BlogDetailsComponent implements OnInit {

  url: string | null = null;
  blogPost$?: Observable<BlogPost>;
  blogPostId?: string;
  user?: UserModel;

  addLikeSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;

    constructor(private route: ActivatedRoute,
      private blogPostService: BlogPostService,
      private authService: AuthService,
      private addLikeCommentService: AddLikeCommentsServiceService) {
    }

  ngOnInit(): void {
    this.route.paramMap
    .subscribe({
      next: (params) => {
        this.url = params.get('url')
      }
    });

    // Fetch blog details by url
    if(this.url) {
      this.blogPost$ = this.blogPostService.getBlogPostByUrlHandle(this.url)

      // Get to know blogPostId
      this.getBlogPostSubscription = this.blogPostService
        .getBlogPostByUrlHandle(this.url)
        .subscribe({
          next: (response) => {
            this.blogPostId = response.id;
          }
        })
    }

    // Get to know if user is registered
    this.authService.user()
   .subscribe({
      next: (response) => {
        this.user = response;
      }
   });

   this.user = this.authService.getUser();
  }

  IsUserLoggedIn():boolean {
    return this.user !== undefined;
  }

  likeButtonClick(): void {
    
    if (this.user && this.blogPostId) {
      const likeBlogPostRequest: AddLikeRequest = {
        userId: this.user.userId,
        blogPostId: this.blogPostId
      };

      this.addLikeSubscription = this.addLikeCommentService.addLike(likeBlogPostRequest)
      .subscribe({
        next: (response) => {

        }
      })
    }
  }
}
