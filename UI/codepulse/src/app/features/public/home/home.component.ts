import { Component, OnInit } from '@angular/core';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPost } from '../../blog-post/models/blog-post.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})


export class HomeComponent implements OnInit {

  filteredBlogs: BlogPost[] = [];

  blogposts: BlogPost[] = [];

  searchName: string = '';

  blogs$?: Observable<BlogPost[]>;

  constructor(private blogPostService: BlogPostService) {
    this.blogs$ = this.blogPostService.getAllBlogPosts();
    this.blogPostService.getAllBlogPosts().subscribe(
     blogs => {
     this.blogposts = blogs;
     this.filteredBlogs = blogs;
   });
  }

  filterByName() {
    console.log(this.filteredBlogs.length)
    if (this.searchName.trim() === '') {
      this.filteredBlogs = this.blogposts;
    } else {
      this.filteredBlogs = this.blogposts.filter(blogpost =>
        blogpost.title.toLowerCase().includes(this.searchName.toLowerCase())
      );
    }
  }

  ngOnInit():void {
     this.blogs$ = this.blogPostService.getAllBlogPosts();
  }

}