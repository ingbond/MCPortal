import { ProjectLineTableViewModel, FilterProjectLinesModel, Pagination, PermissionsService } from './../../../../swagger-services/api.client.generated';
import { Router } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { State } from '@progress/kendo-data-query';
import { ProjectTableDataService } from './project-table-data.service';
import { DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { DomSanitizer } from '@angular/platform-browser';
import { ImageHandlerService } from 'src/app/main/services/image-handler.service';

@Component({
  selector: 'app-projects-table',
  templateUrl: './projects-table.component.html',
  styleUrls: ['./projects-table.component.scss'],
})
export class ProjectsTableComponent implements OnInit {
  projectLines: Observable<ProjectLineTableViewModel[]>;
  projectLinesIsLoading = false;
  state: State = {
    skip: 0,
    take: 18,
    sort: [{ dir: 'asc', field: 'projectName' }],
  };
  currentFilter: FilterProjectLinesModel;

  @Input() set formFilter(filter) {
    if (filter) {
      this.currentFilter = filter;
      this.onSearch();
    }
  }

  @Input() notAllowedProps: string[] = [];

  constructor(
    private router: Router,
    public projectTableDataService: ProjectTableDataService,
    private imageHandlerService: ImageHandlerService
  ) {}

  ngOnInit() {
    this.projectLines = this.projectTableDataService;
  }

  onCellClicked(event) {
    console.log('onPersonClicked', event);
    this.router.navigate(['projects', event.dataItem.id]);
  }

  getImage(model: ProjectLineTableViewModel) {
    if (model.fileDoc) {
      return this.imageHandlerService.getImage(model.fileDoc);
    }

    return null;
  }

  isHidden(columnName: string): boolean {
    if (this.notAllowedProps.indexOf(columnName) >= 0) {
      return true;
    }

    return false;
  }

  onSearch() {
    let model = new FilterProjectLinesModel({
      pagination: new Pagination({
        skip: this.state.skip,
        take: this.state.take,
        dir: this.state.sort ? this.state.sort[0].dir : null,
        field: this.state.sort ? this.state.sort[0].field : null,
      }),
    });
    model = Object.assign(model, this.currentFilter);
    this.projectTableDataService.read(model);
  }

  dataStateChange(state: DataStateChangeEvent): void {
    this.projectTableDataService.state = state;
    this.state = state;
    this.onSearch();
  }
}
