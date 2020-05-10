import { CustomNotificationService } from '../main/services/custom-notification.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../main/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  isSubmitting = false;

  constructor(
    private _formBuilder: FormBuilder,
    private authService: AuthService,
    public router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this._formBuilder.group({
      name: ['', [Validators.required]],
      password: ['', Validators.required],
    });

    if (this.authService.isLoginError) {
      this.loginForm.setValue({
        name: this.loginForm.get('name').value,
        password: '',
      });
    }
  }

  async onSubmit() {
    if (this.loginForm.valid) {
      this.isSubmitting = true;

      try {
        await this.authService
          .login(
            this.loginForm.get('name').value,
            this.loginForm.get('password').value
          )
          .toPromise();

        this.router.navigate(['']);
      } catch (error) {
        console.error('onSubmit', error);
        this.setCurrentValueToForm();
      } finally {
        this.isSubmitting = false;
      }
    } else {
      this.loginForm.markAllAsTouched();
    }
  }

  private setCurrentValueToForm() {
    this.loginForm.setValue({
      name: this.loginForm.get('name').value,
      password: '',
    });
  }
}
