import { AbstractControl, ValidationErrors } from '@angular/forms';
import { phoneRegex } from '@constants/phone-regex';

export const phoneValidator = (control: AbstractControl): ValidationErrors => {
  const value = control.value;
  if (value) {
    const matchValue = String(value).match(phoneRegex);
    if (!matchValue) {
      return {
        phone: true,
      };
    }
  }

  return null;
};
