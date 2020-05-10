import { UserComponent } from './../../layout/views/user/user.component';
import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { CustomNotificationService } from 'src/app/main/services/custom-notification.service';
import { Observable } from 'rxjs';

@Injectable()
export class UserCanDeactivateGuard implements CanDeactivate<UserComponent> {
  
  constructor(
    private customNotificationService: CustomNotificationService) { }

  canDeactivate(
    component: UserComponent,
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {

      if (component.editUserComponent.profileForm.dirty) {
        return this.customNotificationService.confirm('Discard changes for user?');
      }

      return true;
  }
} 