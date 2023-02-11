import { AbstractControl, ValidationErrors } from '@angular/forms';
import { urlRegex } from '@constants/url-regsx';

export const urlValidator = (control: AbstractControl): ValidationErrors => {
  const value = control.value;
  if (value) {
    const matchValue = String(value).match(urlRegex);
    if (!matchValue) {
      return {
        url: true,
      };
    }
  }

  return null;
};
