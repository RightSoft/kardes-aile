import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SnackbarService } from '@appModule/services/snackbar.service';
import { mustMatch } from '@validationModule/confirm-password';
import { CreateModeratorModel } from '../../models/create-moderator-model';
import { ModeratorService } from '../../moderator.service';
import { NavigationService } from '@appModule/services/navigation.service';
import { MatCardModule } from '@angular/material/card';
import BaseListComponent from '@appModule/base-classes/base-list-component.abstract.class';

@Component({
  selector: 'app-moderator-create',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MatCardModule
  ],
  templateUrl: './moderator-create.component.html',
  styleUrls: ['./moderator-create.component.scss']
})
export default class ModeratorCreateComponent
  extends BaseListComponent
  implements OnInit
{
  form: FormGroup;

  constructor(
    private moderatorService: ModeratorService,
    private snackbarService: SnackbarService,
    private formBuilder: FormBuilder,
    private navigationService: NavigationService
  ) {
    super('Moderatör Kayıt');
  }

  ngOnInit() {
    this.form = this.formBuilder.group(
      {
        firstName: this.formBuilder.control('', [
          Validators.required,
          Validators.maxLength(100)
        ]),
        lastName: this.formBuilder.control('', [
          Validators.required,
          Validators.maxLength(100)
        ]),
        email: this.formBuilder.control('', [
          Validators.required,
          Validators.email,
          Validators.maxLength(100)
        ]),
        password: this.formBuilder.control('', [
          Validators.required,
          Validators.maxLength(100)
        ]),
        repassword: this.formBuilder.control('', [
          Validators.required,
          Validators.maxLength(100)
        ])
      },
      { validators: mustMatch('password', 'repassword') }
    );
  }

  onSave() {
    if (this.form.invalid) {
      return;
    }

    const model: CreateModeratorModel = Object.assign({}, this.form.value);

    this.moderatorService.create(model).subscribe(() => {
      this.snackbarService.show(
        'Success',
        `Moderator: ${model.firstName} ${model.lastName} created successfully.`
      );
      this.onCancel();
    });
  }

  onCancel() {
    this.navigationService.navigate('moderator');
  }
}
