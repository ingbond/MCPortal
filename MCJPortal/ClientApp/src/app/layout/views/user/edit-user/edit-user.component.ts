import { ValidationService } from './../../../../main/services/validation.service';
import { tap } from 'rxjs/operators';
import {
  RoleViewModel,
  UsersService,
  ProjectsService,
  ProjectViewModel,
  ListItemViewModel,
} from './../../../../swagger-services/api.client.generated';
import { Observable } from 'rxjs';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import {
  UserViewModel,
  RolesService,
} from 'src/app/swagger-services/api.client.generated';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CustomNotificationService } from 'src/app/main/services/custom-notification.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss'],
})
export class EditUserComponent implements OnInit {
  roles: Observable<RoleViewModel[]>;
  rolesIsLoading = true;
  projects: Observable<ProjectViewModel[]>;
  projectsIsLoading = true;
  profileForm: FormGroup;
  inputUser: UserViewModel;
  selectedProjects = [];
  submitted = false;

  @Input() set user(user: UserViewModel) {
    this.inputUser = user;
    this.selectedProjects = user && user.userProjectIds ? user.userProjectIds : [];
    this.recreateForm(this.inputUser);
  }
  @Input() locations: ListItemViewModel[] = [];
  @Input() locationsIsLoading = true;

  @Output() userChangedEvent = new EventEmitter<boolean>();
  @Output() submittingChangeEvent: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() clearEvent = new EventEmitter();

  constructor(
    private rolesService: RolesService,
    private usersService: UsersService,
    private notificationsService: CustomNotificationService,
    private projectsService: ProjectsService
  ) {}

  ngOnInit() {
    this.loadRoles();
    this.loadProjects();
    this.recreateForm(this.inputUser);
  }

  public onClear() {
    this.submitted = false;
  }

  public async onUpdate() {
    console.log('onUpdate', this.profileForm.valid);
    this.submitted = true;

    if (this.profileForm.valid) {
      this.submittingChangeEvent.next(true);
      const updateModel: UserViewModel = this.profileForm.getRawValue();
      updateModel.id = this.inputUser.id;
      updateModel.userProjectIds = this.selectedProjects;

      try {
        await this.usersService.update(updateModel).toPromise();
        this.notificationsService.show('Success!', 'success');
        this.userChangedEvent.next(true);
      } catch (error) {
        console.log(error);
      } finally {
        this.submittingChangeEvent.next(false);
        this.profileForm.markAsPristine();
      }
    }
  }

  public async onDelete() {
    if (confirm('Are you sure you want to delete?')) {
      this.submittingChangeEvent.next(true);

      try {
        await this.usersService.delete(this.inputUser.id).toPromise();
        this.notificationsService.show('Success!', 'success');
        this.userChangedEvent.next(true);
        this.clearEvent.next();
      } catch (error) {
        console.log(error);
      } finally {
        this.submittingChangeEvent.next(false);
      }
    }
  }

  public async onCreate() {
    this.submitted = true;

    if (this.profileForm.valid) {
      this.submittingChangeEvent.next(true);
      const createModel: UserViewModel = this.profileForm.getRawValue();
      createModel.userProjectIds = this.selectedProjects;

      try {
        await this.usersService.create(createModel).toPromise();
        this.notificationsService.show('Success!', 'success');
        this.userChangedEvent.next(true);
      } catch (error) {
        console.log(error);
      } finally {
        this.submittingChangeEvent.next(false);
        this.profileForm.markAsPristine();
      }
    }
  }

  private loadRoles() {
    this.rolesIsLoading = true;
    this.roles = this.rolesService.getAll().pipe(
      tap(x => this.rolesIsLoading = false)
    );
  }

  private loadProjects() {
    this.projectsIsLoading = true;
    this.projects = this.projectsService.getProjects().pipe(
      tap(x => this.projectsIsLoading = false)
    );
  }

  private recreateForm(user: UserViewModel) {
    this.submitted = false;
    this.profileForm = new FormGroup({
      userName: new FormControl(user ? user.userName : '', Validators.required),
      firstName: new FormControl(user ? user.firstName : '', Validators.required),
      lastName: new FormControl(user ? user.lastName : '', Validators.required),
      password: new FormControl('', user ? null : [Validators.required, ValidationService.password]),
      email: new FormControl(user ? user.email : '', Validators.required),
      roleId: new FormControl(user ? user.roleId : '', Validators.required),
      isActive: new FormControl(user ? user.isActive : false),
      isAllProjectsAllowed: new FormControl(user ? user.isAllProjectsAllowed : true),
      countryId: new FormControl(user ? user.countryId : null, Validators.required)
    });
  }
}
