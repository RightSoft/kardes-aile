import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  ValidatorFn,
  Validators
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router } from '@angular/router';
import { SnackbarService } from '../../../../app/services/snackbar.service';
import { mustMatch } from '../../../validation/confirm-password';
import { ModeratorResult } from '../../models/moderator-result';
import { UpdateModeratorModel } from '../../models/update-moderator-model';
import { ModeratorService } from '../../moderator.service';
import { NavigationService } from '@appModule/services/navigation.service';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-moderator-update',
  standalone: true,
  imports: [
    CommonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MatCardModule
  ],
  templateUrl: './moderator-update.component.html',
  styleUrls: ['./moderator-update.component.scss']
})
export default class ModeratorUpdateComponent implements OnInit {
  form: FormGroup;

  id: string;
  moderator: ModeratorResult;

  get updatePassword() {
    return this.form.get('updatePassword').value as boolean;
  }

  constructor(
    private moderatorService: ModeratorService,
    private snackbarService: SnackbarService,
    private formBuilder: FormBuilder,
    private navigationService: NavigationService,
    private cdRef: ChangeDetectorRef,
    activatedRoute: ActivatedRoute
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id');
  }

  ngOnInit() {
    this.form = this.formBuilder.group(
      {
        fullName: this.formBuilder.control('', [
          Validators.required,
          Validators.maxLength(100)
        ]),
        email: this.formBuilder.control('', [
          Validators.required,
          Validators.email,
          Validators.maxLength(100)
        ]),
        updatePassword: this.formBuilder.control(false),
        password: this.formBuilder.control(''),
        repassword: this.formBuilder.control('')
      },
      { validators: mustMatch('password', 'repassword') }
    );

    this.moderatorService.get(this.id).subscribe((moderator) => {
      this.moderator = moderator;
      this.form.patchValue(this.moderator);
    });
  }

  onUpdatePasswordChange(checked: boolean) {
    const passwordControl = this.form.get('password');
    if (checked) {
      passwordControl?.addValidators([
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(8)
      ]);
      this.form.addValidators(
        mustMatch('password', 'password2') as ValidatorFn
      );
    } else {
      passwordControl?.clearValidators();
      passwordControl?.setErrors(null);
    }
    this.cdRef.detectChanges();
  }

  onCancel() {
    this.navigationService.navigate('moderator');
  }

  onSave() {
    if (this.form.invalid) {
      return;
    }

    const model: UpdateModeratorModel = Object.assign({}, this.form.value);

    this.moderatorService.update(this.id, model).subscribe(() => {
      this.snackbarService.show(
        'Success',
        `Moderator: ${model.fullName} updated successfully.`
      );
      this.onCancel();
    });
  }
}
