import { ProjectLineViewModel, ListItemsService, ListTypeEnum, ListItemViewModel, FilterProjectLinesModel, PermissionsService } from './../../../swagger-services/api.client.generated';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ProjectsService } from 'src/app/swagger-services/api.client.generated';
import { tap } from 'rxjs/operators';
import { ProjectTableDataService } from './projects-table/project-table-data.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss'],
})
export class ProjectsComponent implements OnInit {
  form: FormGroup;
  projectLines: Observable<ProjectLineViewModel[]>;
  projectLinesIsLoading = false;
  filter: FilterProjectLinesModel;

  statuses: Observable<ListItemViewModel[]>;
  statusesIsLoading = false;

  notAllowedProperties: string[] = [];
  notAllowedPropertiesIsLoading = false;

  constructor(private listItemsService: ListItemsService, public projectTableDataService: ProjectTableDataService, 
    private permissionsService: PermissionsService) {}

  ngOnInit() {
    this.getNotAllowedProperties();
    this.createFilterForm();
    this.getStatuses();
    this.getFilterValues();
  }

  getFilterValues() {
    this.filter = this.form.getRawValue();
  }

  checkIfFieldAllowed(fieldName: string) {
    console.log(fieldName, this.notAllowedProperties.map(x => x.toLowerCase()).indexOf(fieldName.toLowerCase()) >= 0);

    return this.notAllowedProperties.map(x => x.toLowerCase()).indexOf(fieldName.toLowerCase()) >= 0;
  }

  private getStatuses() {
    this.statusesIsLoading = true;
    this.statuses = this.listItemsService.getListItems(ListTypeEnum.LineStatus)
    .pipe(tap((x) => (this.statusesIsLoading = false)));
  }

  private async getNotAllowedProperties() {
    try {
      this.notAllowedPropertiesIsLoading = true;
      this.notAllowedProperties = await this.permissionsService.getNotAllowedProperties().toPromise();
      this.disableFormControls(this.form, this.notAllowedProperties);
    } catch (error) {
      console.log(error);
    } finally {
      this.notAllowedPropertiesIsLoading = false;
    }
  }

  private disableFormControls(form: FormGroup, notAllowedProps: string[]) {
    notAllowedProps.forEach(element => {
      switch (element) {
        case 'ProjectName':
          this.form.get('projectName').disable();
          break;

        default:
          break;
      }
    });
  }

  private createFilterForm() {
    this.form = new FormGroup({
      projectName: new FormControl(''),
      lineNumber: new FormControl(''),
      barcode: new FormControl(''),
      nickname: new FormControl(''),
      psl: new FormControl(''),
      statusId: new FormControl(''),
      allocatedTo: new FormControl(''),
    });
  }
}
