import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { getValidationMessage } from '@validationModule/get-validation-message';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'add-child-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule
  ],
  templateUrl: './add-child.component.html',
  styleUrls: ['./add-child.component.scss']
})
export class AddChildComponent {
 
  private formBuilder = inject(FormBuilder);
  addChildForm = this.formBuilder.group({
    name: this.formBuilder.control('', Validators.required),
    birthday: this.formBuilder.control(new Date(), Validators.required),
    gender: this.formBuilder.control('', Validators.required),
  });
  constructor() {}
  public get nameValidationMessage(): string {
    return getValidationMessage(this.addChildForm.controls.name);
  }
  public get birtdayValidationMessage(): string {
    return getValidationMessage(this.addChildForm.controls.birthday);
  }
  public get genderdValidationMessage(): string {
    return getValidationMessage(this.addChildForm.controls.gender);
  }
}
