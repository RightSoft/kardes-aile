import { phoneValidator } from './../../../validation/phone-validator';
import { emailValidator } from './../../../validation/email-validator';
import { getValidationMessage } from '@validationModule/get-validation-message';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import AddPageTitle from '@appModule/base-classes/add-page-title.abstract.class';
import { MatSelectModule } from '@angular/material/select';
import { MatDialog,MatDialogModule } from '@angular/material/dialog';
import { AddChildComponent } from '../add-child/add-child.component';
import { MatTableModule } from '@angular/material/table'  
import { Children } from '@appModule/models/children';

@Component({
  selector: 'app-add-disaster-victim',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatIconModule,
    MatDialogModule,
    MatTableModule
  ],
  templateUrl: './add-disaster-victim.component.html',
  styleUrls: ['./add-disaster-victim.component.scss'],
  host: {
    class: 'row row-cols-2'
  }
})
export default class AddDisasterVictimComponent extends AddPageTitle {
  private dialog = inject(MatDialog);
  private formBuilder = inject(FormBuilder);
  childrens : Array<Children> = [];
  displayedColumns: string[] = ['name', 'birthday', 'gender', 'action'];

  form = this.formBuilder.group({
    tckn: this.formBuilder.control(undefined, [
      Validators.required,
      Validators.min(99999999999),
      Validators.max(99999999999)
    ]),
    fullName: this.formBuilder.control(undefined, Validators.required),
    email: this.formBuilder.control(undefined, [
      Validators.required,
      emailValidator
    ]),
    phone: this.formBuilder.control(undefined, [
      Validators.required,
      phoneValidator
    ]),
    address: this.formBuilder.control(undefined, Validators.required),
    city: this.formBuilder.control(undefined, Validators.required),
    country: this.formBuilder.control(undefined, Validators.required),
    temporaryAddress: this.formBuilder.control(undefined, Validators.required),
    temporaryCity: this.formBuilder.control(undefined, Validators.required),
    temporaryCountry: this.formBuilder.control(undefined, Validators.required),
    picture: this.formBuilder.control(undefined, Validators.required),
    userValidated: this.formBuilder.control(undefined),
    emailValidated: this.formBuilder.control(undefined),
    ssnValidated: this.formBuilder.control(undefined),
    addressValidated: this.formBuilder.control(undefined)
  });
  constructor() {
    super('Afetzede Ekle');
  }
  public get tcknValidationMessage(): string {
    return getValidationMessage(this.form.controls.tckn);
  }
  public get fullNameValidationMessage(): string {
    return getValidationMessage(this.form.controls.fullName);
  }
  public get emailValidationMessage(): string {
    return getValidationMessage(this.form.controls.email);
  }
  public get phoneValidationMessage(): string {
    return getValidationMessage(this.form.controls.phone);
  }
  public get addressValidationMessage(): string {
    return getValidationMessage(this.form.controls.address);
  }
  public get cityValidationMessage(): string {
    return getValidationMessage(this.form.controls.city);
  }
  public get countryValidationMessage(): string {
    return getValidationMessage(this.form.controls.country);
  }
  public get temporaryAddressValidationMessage(): string {
    return getValidationMessage(this.form.controls.temporaryAddress);
  }
  public get temporaryCityValidationMessage(): string {
    return getValidationMessage(this.form.controls.temporaryCity);
  }
  public get temporaryCountryValidationMessage(): string {
    return getValidationMessage(this.form.controls.temporaryCountry);
  }
  public get pictureValidationMessage(): string {
    return getValidationMessage(this.form.controls.picture);
  }
  showAddChildDialog(){
    console.log('yey');
    this.dialog.open(AddChildComponent);
  }
}
