import { EditUserComponent } from './edit-user/edit-user.component';
import {
  tap,
  debounceTime,
  distinctUntilChanged,
} from 'rxjs/operators';
import { Observable } from 'rxjs';
import {
  UsersService,
  UserViewModel,
  ListItemsService,
  CountryService,
  CountryViewModel,
} from './../../../swagger-services/api.client.generated';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { CustomNotificationService } from 'src/app/main/services/custom-notification.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent implements OnInit {
  @ViewChild(EditUserComponent, { static: false })
  editUserComponent: EditUserComponent;

  userData: Observable<UserViewModel[]>;
  userDataIsLoading = true;
  locations: Observable<CountryViewModel[]>;
  locationsIsLoading = true;
  selectedUser: UserViewModel;
  form: FormGroup;

  isSubmitting = false;

  constructor(
    private usersService: UsersService,
    private listItemsService: ListItemsService,
    private customNotificationService: CustomNotificationService,
    private countryService: CountryService
  ) {}

  ngOnInit() {
    this.createFilterForm();
    this.refreshUserData();
    this.loadLocations();
  }

  public async onCellClick({ dataItem }) {
    if (this.editUserComponent.profileForm.dirty) {
      const accept = await this.customNotificationService
      .confirm('Discard changes for user?')
      .toPromise();

      if (!accept) {
        return;
      }
    }
    this.selectedUser = dataItem;
  }

  public onUserChange() {
    this.refreshUserData();
  }

  public onClear() {
    this.selectedUser = null;
    this.editUserComponent.onClear();
  }

  public onSubmittingChange(event: boolean) {
    this.isSubmitting = event;
  }

  private loadLocations() {
    this.locationsIsLoading = true;
    this.locations = this.countryService
      .getCountries()
      .pipe(tap((x) => (this.locationsIsLoading = false)));
  }

  private refreshUserData() {
    this.userDataIsLoading = true;
    const filterModel = this.form.getRawValue();
    this.userData = this.usersService
      .getAll(filterModel.name, filterModel.locationId)
      .pipe(tap((x) => (this.userDataIsLoading = false)));
  }

  private createFilterForm() {
    this.form = new FormGroup({
      name: new FormControl(''),
      locationId: new FormControl(''),
    });
    this.form.valueChanges
      .pipe(debounceTime(800), distinctUntilChanged())
      .subscribe((x) => this.refreshUserData());
  }
}
