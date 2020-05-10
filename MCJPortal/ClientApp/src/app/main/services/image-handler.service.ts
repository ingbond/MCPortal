import { Injectable } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class ImageHandlerService {

constructor(private sanitizer: DomSanitizer) { }

getImage(fileDoc: string): SafeResourceUrl {
  if (fileDoc) {
    const base64Image = 'data:image/png;base64,' + fileDoc;
    return this.sanitizer.bypassSecurityTrustResourceUrl(base64Image);
  }

  return null;
}
}
