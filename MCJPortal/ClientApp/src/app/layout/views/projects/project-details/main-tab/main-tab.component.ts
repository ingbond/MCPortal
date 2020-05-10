import { Observable } from 'rxjs';
import { ProjectLineService, ListItemsService, ListTypeEnum, ListItemViewModel } from './../../../../../swagger-services/api.client.generated';
import { Component, OnInit, Input } from '@angular/core';
import {
  ProjectViewModel,
  ProjectLineViewModel,
} from 'src/app/swagger-services/api.client.generated';
import { ImageHandlerService } from 'src/app/main/services/image-handler.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ValidationService } from 'src/app/main/services/validation.service';
import { tap } from 'rxjs/operators';
import { CustomNotificationService } from 'src/app/main/services/custom-notification.service';

@Component({
  selector: 'app-main-tab',
  templateUrl: './main-tab.component.html',
  styleUrls: ['./main-tab.component.scss'],
})
export class MainTabComponent implements OnInit {
  @Input() projectLine: ProjectLineViewModel;
  mainTabForm: FormGroup;
  isSubmitting = false;

  lineStatuses: Observable<ListItemViewModel[]>;
  lineStatusesIsLoading = false;
  materials: Observable<ListItemViewModel[]>;
  materialsIsLoading = false;

  constructor(
    public imageHandlerService: ImageHandlerService,
    private projectLineService: ProjectLineService,
    private listItemsService: ListItemsService,
    private notificationsService: CustomNotificationService
  ) {}

  ngOnInit() {
    this.initFormGroup(this.projectLine);
    this.getLineStatuses();
    this.getMaterials();
  }

  getLineStatuses() {
    this.lineStatusesIsLoading = false;

    this.lineStatuses = this.listItemsService.getListItems(ListTypeEnum.LineStatus).pipe(
      tap(x => this.lineStatusesIsLoading = false)
    );
  }

  getMaterials() {
    this.materialsIsLoading = false;

    this.materials = this.listItemsService.getListItems(ListTypeEnum.Material).pipe(
      tap(x => this.materialsIsLoading = false)
    );
  }


  onSubmit() {
    this.isSubmitting = true;
    const formValues = this.mainTabForm.getRawValue();
    const resultModel = Object.assign(this.projectLine, formValues);

    console.log(resultModel);
    this.projectLineService.update(resultModel).subscribe(
      data => {
        this.notificationsService.show('Success!', 'success');
      },
      error => {
        console.log(error);
      },
      () => this.isSubmitting = false
    );
  }

  public handleFileSelect(evt) {
    const files = evt.target.files;
    const file = files[0];

    if (files && file) {
      const reader = new FileReader();
      reader.onload = this._handleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
    }
  }

  private _handleReaderLoaded(readerEvt) {
    const binaryString = readerEvt.target.result;
    const base64textString = btoa(binaryString);
    this.projectLine.fileDoc = base64textString;
  }

  private initFormGroup(projectLine: ProjectLineViewModel) {
    this.mainTabForm = new FormGroup({
      description: new FormControl(
        projectLine ? projectLine.description : ''
      ),
      customerDescription: new FormControl(
        projectLine ? projectLine.customerDescription : ''
      ),
      barcode: new FormControl(
        projectLine ? projectLine.barcode : '',
        Validators.required
      ),
      lineNumber: new FormControl(
        projectLine ? projectLine.number : '',
        Validators.required
      ),
      lineStatus: new FormControl('', Validators.required),
      ULN: new FormControl('', Validators.required),
      PSL: new FormControl('', Validators.required),
      oldSystemPartNumber: new FormControl(
        projectLine ? projectLine.oldSystemPartNumber : '',
        Validators.required
      ),
      customerPart: new FormControl(
        projectLine ? projectLine.customerNumber : '',
        Validators.required
      ),
      oemPart: new FormControl(
        projectLine ? projectLine.oemNumber : '',
        Validators.required
      ),
      nicknameIndia: new FormControl(
        projectLine ? projectLine.nickNameIndia : '',
        Validators.required
      ),
      nicknameAustralia: new FormControl(
        projectLine ? projectLine.nickNameAustralia : '',
        Validators.required
      ),
      material: new FormControl(
        projectLine ? projectLine.materialId : '',
        Validators.required
      ),
      weightInGram: new FormControl(
        projectLine ? projectLine.weight : '',
        Validators.required
      ),
      unitPackageQuantity: new FormControl(
        projectLine ? projectLine.unitPackagedQty : '',
        Validators.required
      ),
    });
  }
}
