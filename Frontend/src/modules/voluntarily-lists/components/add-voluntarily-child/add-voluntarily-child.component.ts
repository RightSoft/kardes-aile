import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {MatDialogModule} from '@angular/material/dialog';
import {Component, EventEmitter, Inject, inject, Optional} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {getValidationMessage} from '@validationModule/get-validation-message';
import {MatSelectModule} from '@angular/material/select';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {ChildResultModel} from '@appModule/models/child/child-result.model';
import {ChildModel} from "@appModule/models/child/child.model";
import {Genders, GendersLabel} from "@appModule/models/shared/genders.enum";

@Component({
  selector: 'add-voluntarily-child-dialog',
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
  templateUrl: './add-voluntarily-child.component.html',
  styleUrls: ['./add-voluntarily-child.component.scss']
})
export class AddVoluntarilyChildComponent {
  public genders = Object.values(Genders).filter(v => Number.isFinite(v)) as number[];
  public genderLabels = GendersLabel;
  onSubmitChildEvent = new EventEmitter();
  private formBuilder = inject(FormBuilder);
  addChildForm = this.formBuilder.group({
    id: this.formBuilder.control(undefined),
    name: this.formBuilder.control(undefined, Validators.required),
    birthDate: this.formBuilder.control(new Date(), Validators.required),
    gender: this.formBuilder.control(undefined, Validators.required),
  });

  constructor(
    public dialogRef: MatDialogRef<AddVoluntarilyChildComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public child: ChildResultModel) {
    this.dialogRef = dialogRef;
    this.addChildForm.setValue({
      id: this.child?.id ?? null,
      name: this.child?.name ?? null,
      birthDate: this.child?.birthDate ?? new Date(),
      gender: this.child?.gender ?? null
    });
  }

  public get nameValidationMessage(): string {
    return getValidationMessage(this.addChildForm.controls.name);
  }

  public get birthdayValidationMessage(): string {
    return getValidationMessage(this.addChildForm.controls.birthDate);
  }

  public get genderValidationMessage(): string {
    return getValidationMessage(this.addChildForm.controls.gender);
  }

  public get submitButtonText(): string {
    return this.child?.id !== undefined ? 'Guncelle' : 'Ekle';
  }

  public onSaveChild() {
    if (this.addChildForm.valid) {
      this.onCloseDialog();
      this.onSubmitChildEvent.emit({
        id: this.addChildForm.value.id,
        name: this.addChildForm.value.name,
        birthDate: this.addChildForm.value.birthDate,
        gender: this.addChildForm.value.gender
      } as ChildModel);
    }
  }

  public onCloseDialog() {
    this.dialogRef.close();
  }

}
