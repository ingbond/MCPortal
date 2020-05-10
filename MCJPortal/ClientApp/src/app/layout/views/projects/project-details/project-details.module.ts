import { ListItemsService } from './../../../../swagger-services/api.client.generated';
import { McjSharedModule } from './../../../../main/modules/mcj-shared.module';
import { ImageHandlerService } from 'src/app/main/services/image-handler.service';
import { MainTabComponent } from './main-tab/main-tab.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectDetailsComponent } from './project-details.component';
import { ProjectsService, ProjectLineService } from 'src/app/swagger-services/api.client.generated';
import { LayoutModule } from '@progress/kendo-angular-layout';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';

@NgModule({
  imports: [
    CommonModule,
    LayoutModule,
    McjSharedModule,
    DropDownsModule
  ],
  declarations: [ProjectDetailsComponent, MainTabComponent],
  providers: [ProjectsService, ProjectLineService, ListItemsService]
})
export class ProjectDetailsModule { }
