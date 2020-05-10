import { RoleViewModel } from './../../../swagger-services/api.client.generated';
import { Component, OnInit, ViewChild } from '@angular/core';
import { RolesService } from 'src/app/swagger-services/api.client.generated';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { EditPermissionComponent } from './edit-permission/edit-permission.component';
import { CustomNotificationService } from 'src/app/main/services/custom-notification.service';

@Component({
  selector: 'app-permissions',
  templateUrl: './permissions.component.html',
  styleUrls: ['./permissions.component.scss'],
})
export class PermissionsComponent implements OnInit {
  @ViewChild(EditPermissionComponent, { static: false })
  editPermissionComponent: EditPermissionComponent;

  selectedRole: RoleViewModel;
  roles: RoleViewModel[];
  rolesIsLoading = false;

  isSubmitting = false;
  valueWasChanged = false;

  constructor(
    private rolesService: RolesService,
    private customNotificationService: CustomNotificationService
  ) {}

  ngOnInit() {
    this.getRoles();
  }

  public isSubmittingChange(event: boolean) {
    this.isSubmitting = event;
  }

  public onSave() {
    this.editPermissionComponent.onSave();
  }

  public async onCellClick({ dataItem }) {
    if (this.valueWasChanged) {
      if (
        !(await this.customNotificationService
          .confirm('Discard changes for user?')
          .toPromise())
      ) {
        return;
      }
    }
    this.selectedRole = dataItem;
  }

  private getRoles() {
    this.rolesIsLoading = true;
     this.rolesService
      .getAll()
      .subscribe(
        data => {
          this.roles = data;
          this.selectedRole = data[0];
          this.rolesIsLoading = false;
        }
      );
  }
}
