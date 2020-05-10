import { Injectable } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class ValidationService {
  constructor() {}

  static getValidatorErrorMessage(validatorName: string, validatorValue?: any) {
    const config = {
      required: 'This field is required',
      email: 'The ' + validatorName + ' must contain a valid email address',
      invalidPassword:
        'Password must be at least 6 characters long, and contain a number.',
      minLength: `Minimum length ${validatorValue.requiredLength}`,
      invalidMatch: 'The password and confirm password must match',
    };

    return config[validatorName];
  }

  static password(control: FormControl) {
    // {6,100}           - Assert password is between 6 and 100 characters
    // (?=.*[0-9])       - Assert a string has at least one number
    if (control.value.match(/^(?=.*[0-9])[a-zA-Z0-9!@#$%^&amp;*]{6,100}$/)) {
      return null;
    } else {
      return { invalidPassword: true };
    }
  }

  static match(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.invalidMatch) {
        return;
      }

      // set error on matchingControl if validation fails
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ invalidMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }
}
