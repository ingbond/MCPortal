import { ProjectsTableComponent } from './projects-table/projects-table.component';
import { GridModule } from '@progress/kendo-angular-grid';
import {
  ProjectsService,
  ListItemsService,
  PermissionsService,
  ProjectLineService,
} from './../../../swagger-services/api.client.generated';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectsComponent } from './projects.component';
import { McjSharedModule } from 'src/app/main/modules/mcj-shared.module';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { ProjectDetailsModule } from './project-details/project-details.module';
import { ProjectTableDataService } from './projects-table/project-table-data.service';

@NgModule({
  imports: [
    CommonModule,
    GridModule,
    DropDownsModule,
    ProjectDetailsModule,
    McjSharedModule,
  ],
  declarations: [ProjectsComponent, ProjectsTableComponent],
  providers: [
    ProjectsService,
    ListItemsService,
    ProjectTableDataService,
    PermissionsService,
    ProjectLineService,
  ],
})
export class ProjectsModule {}
