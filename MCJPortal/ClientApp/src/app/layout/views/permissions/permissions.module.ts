import { ListItemsService } from './../../../swagger-services/api.client.generated';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PermissionsComponent } from './permissions.component';
import { GridModule } from '@progress/kendo-angular-grid';
import { McjSharedModule } from 'src/app/main/modules/mcj-shared.module';
import { RolesService } from 'src/app/swagger-services/api.client.generated';
import { EditPermissionComponent } from './edit-permission/edit-permission.component';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';

@NgModule({
  imports: [
    CommonModule,
    GridModule,
    DropDownsModule,
    McjSharedModule
  ],
  declarations: [PermissionsComponent, EditPermissionComponent],
  providers: [RolesService, ListItemsService]
})
export class PermissionsModule { }
