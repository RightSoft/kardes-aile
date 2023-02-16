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
import { ChildResultModel } from '@appModule/models/child/child-result.model';
import { Genders, GendersLabel } from '@appModule/models/shared/genders.enum';

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
  genders = Object.values(Genders).filter(v => Number.isFinite(v)) as number[];
  children: ChildResultModel;
  action: string;
  private formBuilder = inject(FormBuilder);
  addChildForm = this.formBuilder.group({
    id: this.formBuilder.control(undefined),
    name: this.formBuilder.control(undefined, Validators.required),
    birthDate: this.formBuilder.control(new Date(), Validators.required),
    gender: this.formBuilder.control(undefined, Validators.required),
  });

  constructor(
    public dialogRef: MatDialogRef<AddChildComponent>,
    //@Optional() is used to prevent error if no data is passed
    @Optional() @Inject(MAT_DIALOG_DATA) public data: { child: ChildResultModel, action: string }) {
    this.dialogRef = dialogRef;
    this.children = data.child;
    console.log('data',data);
    this.addChildForm.patchValue(this.children)
    if(!this.addChildForm.value.id) this.addChildForm.removeControl('id');
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
  public getGendersLabel(gender: number): string {
    return GendersLabel.get(gender);
  }
  public closeDialog() {
    this.dialogRef.close({ event: 'Cancel' });
  }
}
