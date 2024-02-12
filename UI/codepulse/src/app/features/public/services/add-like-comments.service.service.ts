import { Injectable } from '@angular/core';
import { BlogPostLike } from '../models/add-like.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { BlogPostComment } from '../models/add-comment.model';

@Injectable({
  providedIn: 'root'
})

export class AddLikeCommentsServiceService {

  constructor(private http: HttpClient) { }

  addLike(blogPostLike: BlogPostLike): Observable<BlogPostLike> {
    return this.http.post<BlogPostLike>(`${environment.apiBaseUrl}/api/blogpostlike/add?addAuth=true`, blogPostLike);
  }

  removeLike(blogPostLike: BlogPostLike): Observable<BlogPostLike> {
    return this.http.post<BlogPostLike>(`${environment.apiBaseUrl}/api/blogpostlike/remove?addAuth=true`, blogPostLike);
  }

  addComment(addCommentRequest: BlogPostComment): Observable<BlogPostComment> {
    return this.http.post<BlogPostComment>(`${environment.apiBaseUrl}/api/blogpostcomment/add?addAuth=true`, addCommentRequest);
  }
}
