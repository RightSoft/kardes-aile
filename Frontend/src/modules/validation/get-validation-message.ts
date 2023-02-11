import { AbstractControl } from '@angular/forms';
import {
  validationMessages,
  validationMessagesFunctions,
} from './validation-messages';

export const getValidationMessage = (control: AbstractControl) => {
  const errors = control.errors;
  const errorsKeys = errors && Object.keys(errors);
  if (errorsKeys) {
    switch (errorsKeys[0]) {
      case 'minlength':
        return validationMessagesFunctions.minLength(
          errors['minlength'].requiredLength
        );
      case 'maxlength':
        return validationMessagesFunctions.maxLength(
          errors['maxlength'].requiredLength
        );
      case 'max':
        return validationMessagesFunctions.max(errors['max'].max);
      case 'min':
        return validationMessagesFunctions.min(errors['min'].min);
      case 'pattern':
        return validationMessagesFunctions.pattern(
          errors['pattern'].requiredPattern
        );
      default:
        return validationMessages[errorsKeys[0]];
    }
  }
  return null;
};
