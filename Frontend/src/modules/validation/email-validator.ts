import { AbstractControl, ValidationErrors } from '@angular/forms';
import { emailRegEx } from '@constants/email-regex';

export const emailValidator = (control: AbstractControl): ValidationErrors => {
  const value = control.value;
  if (value) {
    const matchValue = String(value).match(emailRegEx);
    if (!matchValue) {
      return {
        email: true,
      };
    }
  }

  return null;
};
