import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { SnackbarService } from '../../../../app/services/snackbar.service';
import { mustMatch } from '../../../validation/confirm-password';
import { CreateModeratorModel } from '../../models/create-moderator-model';
import { ModeratorService } from '../../moderator.service';

@Component({
  selector: 'app-moderator-create',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    FlexLayoutModule
  ],
  templateUrl: './moderator-create.component.html',
  styleUrls: ['./moderator-create.component.scss']
})
export default class ModeratorCreateComponent implements OnInit {

  form: FormGroup;

  constructor(
    private moderatorService: ModeratorService,
    private snackbarService: SnackbarService,
    private formBuilder: FormBuilder,
    private route: Router
  ) {

  }

  ngOnInit() {

    this.form = this.formBuilder.group({
      'fullName': this.formBuilder.control('', [Validators.required, Validators.maxLength(100)]),
      'email': this.formBuilder.control('', [Validators.required, Validators.email, Validators.maxLength(100)]),
      'password': this.formBuilder.control('', [Validators.required, Validators.maxLength(100)]),
      'repassword': this.formBuilder.control('', [Validators.required, Validators.maxLength(100)])
    }, { validators: mustMatch('password', 'repassword') });

  }

  onCancel() {
    this.route.navigate(['moderator/']);
  }

  onSave() {

    if (this.form.invalid) {
      return;
    }

    const model: CreateModeratorModel = Object.assign({}, this.form.value);

    this.moderatorService
      .create(model)
      .subscribe(() => {
        this.snackbarService.show('Success', `Moderator: ${model.fullName} created successfully.`);
      });
  }
}
