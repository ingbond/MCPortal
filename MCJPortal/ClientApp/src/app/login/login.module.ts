import { McjSharedModule } from 'src/app/main/modules/mcj-shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { LayoutRoutingModule } from '../layout/layout-routing.module';

@NgModule({
  imports: [
    RouterModule.forChild([
      {path: '', component: LoginComponent, pathMatch: 'full'}
    ]),
    CommonModule,
    ReactiveFormsModule
  ],
  declarations: [LoginComponent]
})
export class LoginModule { }
