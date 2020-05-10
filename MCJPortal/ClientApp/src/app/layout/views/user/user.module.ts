import {
  ProjectsService,
  ListItemsService,
  CountryService,
} from './../../../swagger-services/api.client.generated';
import { EditUserComponent } from './edit-user/edit-user.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user.component';
import {
  RolesService,
} from 'src/app/swagger-services/api.client.generated';
import { GridModule } from '@progress/kendo-angular-grid';
import { McjSharedModule } from 'src/app/main/modules/mcj-shared.module';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: UserComponent
      },
    ]),
    CommonModule,
    GridModule,
    DropDownsModule,
    McjSharedModule
  ],
  declarations: [UserComponent, EditUserComponent],
  providers: [RolesService, ProjectsService, ListItemsService, CountryService],
})
export class UserModule {}
