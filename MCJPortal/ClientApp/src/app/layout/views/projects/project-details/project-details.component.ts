import { ProjectsService, ProjectViewModel, ProjectLineViewModel, ProjectLineService } from 'src/app/swagger-services/api.client.generated';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.scss']
})
export class ProjectDetailsComponent implements OnInit {
  projectLine: ProjectLineViewModel;
  projectLineIsLoading = false;
  private id: number;

  constructor(private activatedRoute: ActivatedRoute,
    private projectsService: ProjectLineService) { }

  ngOnInit() {
    this.id = this.activatedRoute.snapshot.params['id'];
    this.getProjectDetails(this.id);
  }

  public onTabSelect(e) {
    console.log(e);
  }

  private async getProjectDetails(id: number) {
    try {
      this.projectLineIsLoading = true;
      this.projectLine = await this.projectsService.getProjectLine(id).toPromise();
      this.projectLineIsLoading = false;
    } catch (error) {
      console.log(error);
    } finally {

    }
  }
}
