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
  
  blogPost?: BlogPost;
  user?: UserModel;
  isLikedByUser: boolean = false;
  totalLikes?: number;

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


    // Get to know if user is registered
    this.authService.user()
   .subscribe({
      next: (response) => {
        this.user = response;
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
            this.blogPost = response;
            this.totalLikes = response.likes.length;
            if(this.user) {
              this.isLikedByUser = response.likes.includes(this.user?.userId);
            }
          }
        })
    }

   this.user = this.authService.getUser();
  }

  IsUserLoggedIn():boolean {
    return this.user !== undefined;
  }

  likeButtonClick(): void {
    
    if (this.user && this.blogPost) {
      const likeBlogPostRequest: AddLikeRequest = {
        userId: this.user.userId,
        blogPostId: this.blogPost.id
      };

      this.addLikeSubscription = this.addLikeCommentService.addLike(likeBlogPostRequest)
      .subscribe({
        next: (response) => {
          if(this.totalLikes) {
            this.totalLikes++;
            this.isLikedByUser = true;
            // TODO: fix so no reload is needed to change like button and totalLikes and forbid click again
          }
        }
      })
    }
  }
}
