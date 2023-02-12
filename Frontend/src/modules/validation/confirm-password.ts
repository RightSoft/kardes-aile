import { FormGroup, ValidationErrors } from '@angular/forms';

export const confirmPassword = (form: FormGroup): ValidationErrors => {
  const rePasswordControl = form.get('rePassword');
  const password = form.get('password').value;
  const rePassword = rePasswordControl.value;
  const error = {
    confirmPassword: true
  };
  if (password !== rePassword) {
    rePasswordControl.setErrors({
      ...error,
      ...rePasswordControl.errors
    });
    return error;
  }
  rePasswordControl.setErrors(null);
  return null;
};

export function mustMatch(controlName: string, matchingControlName: string) {
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[controlName];
    const matchingControl = formGroup.controls[matchingControlName];

    if (matchingControl.errors && !matchingControl.errors['mustMatch']) {
      return;
    }

    if (control.value !== matchingControl.value) {
      matchingControl.setErrors({ mustMatch: true });
    } else {
      matchingControl.setErrors(null);
    }
  }
}
