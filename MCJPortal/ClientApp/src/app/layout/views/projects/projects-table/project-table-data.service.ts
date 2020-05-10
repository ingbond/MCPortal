import { FilterProjectLinesModel, ProjectLineTableViewModel, ProjectLineService } from './../../../../swagger-services/api.client.generated';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { State, toDataSourceRequestString } from '@progress/kendo-data-query';
import { tap, map } from 'rxjs/operators';
import { ProjectsService } from 'src/app/swagger-services/api.client.generated';

@Injectable({
  providedIn: 'root'
})
export class ProjectTableDataService extends BehaviorSubject<ProjectLineTableViewModel[]> {

  constructor(private projectsService: ProjectLineService) {
    super([]);
  }
  private data: any[] = [];
  // default value, overwritten in injected place
  public state: State = {
    skip: 0,
    take: 5,
    filter: { filters: [], logic: 'or' },
    group: [],
    sort: []
  };
  public loadingSubject = new BehaviorSubject<boolean>(false);

  public read(data: FilterProjectLinesModel) {
    if (this.data.length) {
      return super.next(this.data);
    }

    this.loadingSubject.next(true);
    this.fetch(data)
      .pipe(
        tap(data => {
          this.data = data;
        })
      )
      .subscribe(data => {
        this.loadingSubject.next(false);
        super.next(data);
      },
        error => this.loadingSubject.next(false)
      );
  }

  public fetch(dataItem: FilterProjectLinesModel, action: string = ''): Observable<any> {
    switch (action) {
      case '': {
        return this.projectsService.getProjectLines(dataItem).pipe(
            map( data => {
                return {
                    data: data.data,
                    total: data.total
                };
            }
        ));
      }
    }
  }

}

