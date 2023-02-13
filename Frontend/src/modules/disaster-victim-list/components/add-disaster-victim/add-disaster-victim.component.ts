import { DisasterVictimService } from '@disasterVictimListsModule/business/disaster-victim.service';
import { NavigationService } from '@appModule/services/navigation.service';
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
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { AddChildComponent } from '../add-child/add-child.component';
import { MatTableModule } from '@angular/material/table'
import { Children } from '@appModule/models/children';
import { CreateDisasterVictimModel } from '@appModule/models/create-disaster-victim.model';
import { UpdateDisasterVictimModel } from '@appModule/models/update-disaster-victim.model';
import { CreateChildModel } from '@appModule/models/create-child.model';
import { Genders } from '@appModule/models/shared/genders.enum';
import { AddressService } from '@appModule/services/address.service';
import { CountryResultModel } from '@appModule/models/country-result.model';
import { ChildResultModel } from '@appModule/models/child-result.model';
import { CityResultModel } from '@appModule/models/city-result.model';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
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
  countryList$: CountryResultModel[];
  cityList$: CityResultModel[];
  tempCityList$: CityResultModel[];
  childrens: Array<CreateChildModel> = [];
  displayedColumns: string[] = ['name', 'birthDate', 'gender', 'action'];
  disasterVictimId: string = undefined;
  constructor(private disasterVictimService: DisasterVictimService, private addressService: AddressService, private navigationService: NavigationService, private route: ActivatedRoute, private location: Location) {
    const id = route.snapshot.paramMap.get('id');
    super(id ? 'Afetzede Güncelle' : 'Afetzede Kayit');
    if (id) {
      this.disasterVictimId = id;
      disasterVictimService.get(id).subscribe(result => {
        this.addressService.cities(result.countryId).subscribe((cResult) => {
          console.log(result);
          this.cityList$ = cResult.sort((a, b) => a.name.localeCompare(b.name));;
          this.form.patchValue(result);
          this.form.patchValue({fullName : `${result.firstName} ${result.lastName}`})
        });

      });
    }
  }
  form = this.formBuilder.group({
    id: this.disasterVictimId,
    identityNumber: this.formBuilder.control(undefined, [
      Validators.required,
      Validators.min(9999999999),
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
    cityId: this.formBuilder.control(undefined, Validators.required),
    countryId: this.formBuilder.control(undefined, Validators.required),
    temporaryAddress: this.formBuilder.control(undefined, Validators.required),
    temporaryCityId: this.formBuilder.control(undefined, Validators.required),
    identityNumberValidated: this.formBuilder.control(undefined),
    addressValidated: this.formBuilder.control(undefined),
  });

  ngAfterViewInit() {
    this.addressService.countries().subscribe((result) => {
      console.log(result);
      this.countryList$ = result.sort((a, b) => a.name.localeCompare(b.name));;
    });
  }
  public get countries(): ReadonlyArray<CountryResultModel> {
    return this.countryList$;
  }

  public get tcknValidationMessage(): string {
    return getValidationMessage(this.form.controls.identityNumber);
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
    return getValidationMessage(this.form.controls.cityId);
  }
  public get countryValidationMessage(): string {
    return getValidationMessage(this.form.controls.countryId);
  }
  public get temporaryAddressValidationMessage(): string {
    return getValidationMessage(this.form.controls.temporaryAddress);
  }
  public get temporaryCityValidationMessage(): string {
    return getValidationMessage(this.form.controls.temporaryCityId);
  }


  public calculateAge(birthDate: Date): string {
    var timeDiff = Math.abs(Date.now() - new Date(birthDate).getTime());
    return Math.floor(timeDiff / (1000 * 3600 * 24) / 365.25).toString();
  }
  public getGender(gender: number): string {
    return Object.values(Genders)[gender].toString();
  }
  public showChildDialog(action: string, obj: ChildResultModel, index: number) {
    this.dialog.open(AddChildComponent, { data: { action, obj } }).afterClosed().subscribe(result => {
      if (!result) return;
      if (result.event == 'Add') {
        this.childrens = [...this.childrens, result.data]
      } else if (result.event == 'Update') {
        this.childrens[index] = result.data;
        this.childrens = [...this.childrens];
      }
    });
  }
  public removeChild(child: ChildResultModel) {
    this.childrens = this.childrens.filter((el) => el !== child);
  }
  public updateChild(child: ChildResultModel) {
    const index = this.childrens.findIndex((el) => el === child);
    this.showChildDialog('Update', child, index);
  }

  public countryChanged(id: string) {
    this.addressService.cities(id).subscribe((result) => {
      console.log(result);
      this.cityList$ = result.sort((a, b) => a.name.localeCompare(b.name));;
    });
  }

  submit() {
    console.log(this.form.value);
    this.childrens = this.childrens.map((el) => {
      el.gender = Number(el.gender);
      return el;
    });
    if (this.form.valid) {

      if (!this.disasterVictimId) {
        const model = {
          firstName: this.form.value.fullName.split(' ')[0],
          lastName: this.form.value.fullName.split(' ')[1],
          email: this.form.value.email,
          phone: this.form.value.phone,
          address: this.form.value.address,
          cityId: this.form.value.cityId,
          countryId: this.form.value.countryId,
          temporaryAddress: this.form.value.temporaryAddress,
          identityNumber: this.form.value.identityNumber.toString(),
          identityNumberValidated: this.form.value.identityNumberValidated,
          children: this.childrens
        } as CreateDisasterVictimModel;
        this.disasterVictimService.create(model).subscribe(() => {
          this.backToList();
        });
      }
      else {
        const updateModel = {
          id: this.disasterVictimId,
          firstName: this.form.value.fullName.split(' ')[0],
          lastName: this.form.value.fullName.split(' ')[1],
          email: this.form.value.email,
          phone: this.form.value.phone,
          address: this.form.value.address,
          cityId: this.form.value.cityId,
          countryId: this.form.value.countryId,
          temporaryAddress: this.form.value.temporaryAddress,
          temporaryCityId: this.form.value.temporaryCityId,
          identityNumber: this.form.value.identityNumber.toString(),
          identityNumberValidated: this.form.value.identityNumberValidated,
          status: 0,
          children: this.childrens
        } as UpdateDisasterVictimModel;
        console.log('hereee');
        this.disasterVictimService.update(updateModel).subscribe(() => {
          this.backToList();
        });
      }
    }
  }
  public backToList() {
    this.navigationService.navigate('/disaster-victim-list');
  }
}
