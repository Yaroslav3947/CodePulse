import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { Observable, Subscription } from 'rxjs';
import { BlogPost } from '../../blog-post/models/blog-post.model';
import { AuthService } from '../../auth/services/auth.service';
import { UserModel } from '../../auth/models/user.model';
import { BlogPostLike as BlogPostLike } from '../models/add-like.model';
import { AddLikeCommentsServiceService } from '../services/add-like-comments.service.service';
import { TimeAgoPipe } from '../pipes/time-ago.pipe';
import { BlogPostComment } from '../models/add-comment.model';

@Component({
  selector: 'app-blog-details',
  // standalone: true,
  // imports: [],
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.css'],
})
export class BlogDetailsComponent implements OnInit, OnDestroy {

  url: string | null = null;
  blogPost$?: Observable<BlogPost>;

  blogPost?: BlogPost;
  user?: UserModel;
  isLikedByUser: boolean = false;
  totalLikes?: number;
  commentDescription?: string;

  addLikeSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;
  addCommentSubscription?: Subscription;
  removeLikeSubscription?: Subscription;

    constructor(private route: ActivatedRoute,
      private blogPostService: BlogPostService,
      private authService: AuthService,
      private router: Router,
      private addLikeCommentService: AddLikeCommentsServiceService) {

    }
  ngOnDestroy(): void {
    this.addLikeSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
    this.addCommentSubscription?.unsubscribe();
    this.removeLikeSubscription?.unsubscribe();
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
      const likeBlogPostRequest: BlogPostLike = {
        userId: this.user.userId,
        blogPostId: this.blogPost.id
      };

    if(!this.isLikedByUser) {
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
    } else {
        this.removeLikeSubscription = this.addLikeCommentService.removeLike(likeBlogPostRequest)
        .subscribe({
          next: (response) => {
            if(this.totalLikes && this.totalLikes > 0) {
              this.totalLikes--;
              this.isLikedByUser = false;
            }
          }
        })
      }
    }
  }

  onFormSubmit(): void {
    // Conver model to Request Object
    if (this.blogPost && this.commentDescription && this.user) {
      const blogPostComment: BlogPostComment = {
        blogpostId: this.blogPost.id,
        userId: this.user.userId,
        description: this.commentDescription,
        dateAdded: new Date()
      };

      this.addCommentSubscription = this.addLikeCommentService
        .addComment(blogPostComment)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl(`blog/${this.blogPost?.urlHandle}`);

            ////TODO: page is refreshed and updated comments should be shown
          }
        })
    }
  }

}
