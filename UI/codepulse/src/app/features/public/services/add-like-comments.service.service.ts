import { Injectable } from '@angular/core';
import { AddLikeRequest } from '../models/add-like.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { BlogPostComment } from '../models/add-comment.model';

@Injectable({
  providedIn: 'root'
})

export class AddLikeCommentsServiceService {

  constructor(private http: HttpClient) { }

  addLike(addLikeRequst: AddLikeRequest): Observable<AddLikeRequest> {
    return this.http.post<AddLikeRequest>(`${environment.apiBaseUrl}/api/blogpostlike/add?addAuth=true`, addLikeRequst);
  }

  addComment(addCommentRequest: BlogPostComment): Observable<BlogPostComment> {
    return this.http.post<BlogPostComment>(`${environment.apiBaseUrl}/api/blogpostcomment/add?addAuth=true`, addCommentRequest);
  }
}
