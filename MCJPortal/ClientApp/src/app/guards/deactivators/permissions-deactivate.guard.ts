import { UserComponent } from '../../layout/views/user/user.component';
import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { CustomNotificationService } from 'src/app/main/services/custom-notification.service';
import { Observable } from 'rxjs';
import { PermissionsComponent } from 'src/app/layout/views/permissions/permissions.component';

@Injectable()
export class PermissionsCanDeactivateGuard implements CanDeactivate<PermissionsComponent> {

  constructor(
    private customNotificationService: CustomNotificationService) { }

  canDeactivate(
    component: PermissionsComponent,
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {
    console.log(component);
      if (component.valueWasChanged) {
        return this.customNotificationService.confirm('Discard changes for permissions?');
      }

      return true;
  }
}
