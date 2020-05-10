import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ValidationService } from '../services/validation.service';
import { ErrorMessageComponent } from '../components/error-message/error-message.component';
import { ImageHandlerService } from '../services/image-handler.service';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule],
  declarations: [ErrorMessageComponent],
  exports: [ReactiveFormsModule, CommonModule, FormsModule, ErrorMessageComponent],
  providers: [ValidationService, ImageHandlerService]
})
export class McjSharedModule {}
