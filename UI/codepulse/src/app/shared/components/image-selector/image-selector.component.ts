import { Component } from '@angular/core';
import { ImageService } from './image.service';

@Component({
  selector: 'app-image-selector',
  templateUrl: './image-selector.component.html',
  styleUrls: ['./image-selector.component.css']
})
export class ImageSelectorComponent {
  private file?: File;
  fileName: string = '';
  title: string = '';

  constructor(private imageService: ImageService) {
    
  }


  onFileUploadChange(event: Event): void {
    const element = event.currentTarget as HTMLInputElement
    this.file = element.files?.[0];
  }

  uploadImage(): void {
    if(this.file && this.fileName !== '' && this.title !== '') {
      this.imageService.uploadImage(this.file, this.fileName, this.title)
      .subscribe({
        next: (response) => {
          console.log(response);
        }
      });
    }
  }
  
}
