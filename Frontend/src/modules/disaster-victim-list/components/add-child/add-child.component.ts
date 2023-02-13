import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { Component, Inject, inject, Optional } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { getValidationMessage } from '@validationModule/get-validation-message';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ChildResultModel } from '@appModule/models/child-result.model';
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
    MatSelectModule,
    MatDatepickerModule,
  ],
  templateUrl: './add-child.component.html',
  styleUrls: ['./add-child.component.scss']
})
export class AddChildComponent {

  private formBuilder = inject(FormBuilder);
  children: ChildResultModel;
  action: string;
  addChildForm = this.formBuilder.group({
    name: this.formBuilder.control(undefined, Validators.required),
    birthDate: this.formBuilder.control(new Date(), Validators.required),
    gender: this.formBuilder.control(undefined, Validators.required),
  });
  constructor(
    public dialogRef: MatDialogRef<AddChildComponent>,
    //@Optional() is used to prevent error if no data is passed
    @Optional() @Inject(MAT_DIALOG_DATA) public data: { obj: ChildResultModel, action: string }) {
    this.dialogRef = dialogRef;
    this.children = data.obj;
    console.log(data);
    this.addChildForm.setValue({
      name: this.children?.name ?? null,
      birthDate: this.children?.birthDate ?? new Date(),
      gender: this.children?.gender ?? null
    })
    this.action = this.data.action;
  }

  public get nameValidationMessage(): string {
    return getValidationMessage(this.addChildForm.controls.name);
  }
  public get birtdayValidationMessage(): string {
    return getValidationMessage(this.addChildForm.controls.birthDate);
  }
  public get genderdValidationMessage(): string {
    return getValidationMessage(this.addChildForm.controls.gender);
  }
  public get submitButtonText(): string {
    return this.children?.name != undefined ? 'Guncelle' : 'Ekle';
  }
  public doAction() {
    if (this.addChildForm.valid)
      this.dialogRef.close({ event: this.action, data: this.addChildForm.value });
  }

  public closeDialog() {
    this.dialogRef.close({ event: 'Cancel' });
  }
}
