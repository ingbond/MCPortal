import {
  RoleViewModel,
  ListItemViewModel,
  RolesService,
  AccessEnum,
  EditPermissionViewModel,
} from './../../../../swagger-services/api.client.generated';
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { CustomNotificationService } from 'src/app/main/services/custom-notification.service';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-edit-permission',
  templateUrl: './edit-permission.component.html',
  styleUrls: ['./edit-permission.component.scss'],
})
export class EditPermissionComponent implements OnInit {
  permissions: EditPermissionViewModel[];
  permissionsIsLoading = false;

  accessEnums: { id: number; name: string }[];

  @Output() isSubmittingEvent = new EventEmitter<boolean>();
  @Output() valueWasChangedSubject = new BehaviorSubject<boolean>(false);

  @Input() roleModel: RoleViewModel;
  @Input() set role(role: RoleViewModel) {
    this.roleModel = role;
    this.getPermissionItemsForRole();
    this.valueWasChangedSubject.next(false);
  }

  constructor(
    private rolesService: RolesService,
    private notificationsService: CustomNotificationService
  ) {}

  ngOnInit() {
    this.accessEnums = this.getAccessEnumObject();
  }

  public getSelectedValue(
    permission: ListItemViewModel
  ): { id: number; name: string } {
    const result = permission.permissions.find(
      (x) => x.permissionId === permission.id
    );

    if (result) {
      return this.accessEnums.find((x) => x.id === result.access);
    }

    return null;
  }

  public onSave() {
    this.isSubmittingEvent.next(true);
    this.rolesService
      .setPermission(this.permissions, this.roleModel.id)
      .subscribe(
        (x) => {
          console.log(x);
          this.notificationsService.show('Success!', 'success');
        },
        (error) => {
          this.notificationsService.show('Error!', 'error');
        },
        () => {
          this.isSubmittingEvent.next(false);
          this.valueWasChangedSubject.next(false);
        }
      );
  }

  private getAccessEnumObject(): { id: number; name: string }[] {
    const map: { id: number; name: string }[] = [];

    for (const enu in AccessEnum) {
      if (typeof AccessEnum[enu] === 'number') {
        map.push({ id: <any>AccessEnum[enu], name: enu });
      }
    }
    return map;
  }

  private async getPermissionItemsForRole() {
    this.permissionsIsLoading = true;
    try {
      this.permissions = await this.rolesService
        .getAllPermissions(this.roleModel.id)
        .toPromise();
    } catch (error) {
      console.log(error);
    } finally {
      this.permissionsIsLoading = false;
    }
  }
}
