import { DashboardModule } from './views/dashboard/dashboard.module';
import { McjSharedModule } from 'src/app/main/modules/mcj-shared.module';
import { ProjectsModule } from './views/projects/projects.module';
import { UserCanDeactivateGuard } from './../guards/deactivators/user-deactivate.guard';
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { LayoutRoutingModule } from './layout-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout.component';
import { UserModule } from './views/user/user.module';
import { PermissionsModule } from './views/permissions/permissions.module';
import { PermissionsCanDeactivateGuard } from '../guards/deactivators/permissions-deactivate.guard';
import { MenuModule } from '@progress/kendo-angular-menu';

@NgModule({
  imports: [CommonModule, LayoutRoutingModule, UserModule, DashboardModule, PermissionsModule, ProjectsModule, MenuModule, McjSharedModule],
  declarations: [LayoutComponent, SidebarComponent, HeaderComponent],
  providers: [UserCanDeactivateGuard, PermissionsCanDeactivateGuard],
})
export class LayoutModule {}
