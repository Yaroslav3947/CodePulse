import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogImage } from '../../models/blog-image.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http: HttpClient) { }

  uploadImage(file: File, fileName: string, title: string): Observable<BlogImage> {
    const formData = new FormData();
    formData.append('file', file);
    formData.append('fileName',fileName);
    formData.append('title',title);

    return this.http.post<BlogImage>(`${environment.apiBaseUrl}/api/images`, formData);
  }
}
