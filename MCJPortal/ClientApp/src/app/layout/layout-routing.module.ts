import { DashboardComponent } from './views/dashboard/dashboard.component';
import { ProjectDetailsComponent } from './views/projects/project-details/project-details.component';
import { ProjectsComponent } from './views/projects/projects.component';
import { UserComponent } from './views/user/user.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { PermissionsComponent } from './views/permissions/permissions.component';
import { UserCanDeactivateGuard } from '../guards/deactivators/user-deactivate.guard';
import { PermissionsCanDeactivateGuard } from '../guards/deactivators/permissions-deactivate.guard';
import { AuthGuard } from '../guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'users',
        component: UserComponent,
        canDeactivate: [UserCanDeactivateGuard]
      },
      {
        path: 'permissions',
        component: PermissionsComponent,
        canDeactivate: [PermissionsCanDeactivateGuard]
      },
      {
        path: 'dashboard',
        component: DashboardComponent
      },
      {
        path: 'projects',
        component: ProjectsComponent
      },
      {
        path: 'projects/:id',
        component: ProjectDetailsComponent
      }
     ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LayoutRoutingModule {}
