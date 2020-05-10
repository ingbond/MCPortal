import { Injectable } from '@angular/core';
import { NotificationService } from '@progress/kendo-angular-notification';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CustomNotificationService {
  constructor(private notificationService: NotificationService) {}

  public show(
    message: string,
    showStyle: 'none' | 'success' | 'warning' | 'error' | 'info'
  ): void {
    this.notificationService.show({
      content: message,
      cssClass: 'button-notification',
      animation: { type: 'slide', duration: 400 },
      position: { horizontal: 'center', vertical: 'bottom' },
      type: { style: showStyle, icon: true },
      hideAfter: 2000
    });
  }

  confirm(message?: string): Observable<boolean> {
    const confirmation = window.confirm(message || 'Are you sure?');

    return of(confirmation);
  };
}
