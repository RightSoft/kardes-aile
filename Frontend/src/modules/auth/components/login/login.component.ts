import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  UntypedFormControl,
  UntypedFormGroup,
  Validators
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '@appModule/services/authentication.service';
import { NavigationService } from '@appModule/services/navigation.service';
import { AuthenticationRequest } from '@appModule/models/authentication-request';
import { first } from 'rxjs';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  host: { class: 'd-block login-page h-100' }
})
export default class LoginComponent {
  form: UntypedFormGroup = new UntypedFormGroup({
    email: new UntypedFormControl('', [Validators.email, Validators.required]),
    password: new UntypedFormControl('', Validators.required)
  });

  constructor(
    private route: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private navigationService: NavigationService
  ) {
    if (this.authenticationService.authenticationValue) {
      this.navigationService.navigate('/');
    }
  }

  login() {
    if (this.form.valid) {
      const model = {
        email: this.form.value.email.trim(),
        password: this.form.value.password
      } as AuthenticationRequest;

      this.authenticationService
        .login(model)
        .pipe(first())
        .subscribe(() => {
          const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
          this.navigationService.navigate(returnUrl);
        });
    }
  }
}
