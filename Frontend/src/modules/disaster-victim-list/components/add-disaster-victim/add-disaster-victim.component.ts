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
    MatIconModule
  ],
  templateUrl: './add-disaster-victim.component.html',
  styleUrls: ['./add-disaster-victim.component.scss'],
  host: {
    class: 'row row-cols-2'
  }
})
export default class AddDisasterVictimComponent extends AddPageTitle {
  private formBuilder = inject(FormBuilder);
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
    picture: this.formBuilder.control(undefined, Validators.required)
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
}
