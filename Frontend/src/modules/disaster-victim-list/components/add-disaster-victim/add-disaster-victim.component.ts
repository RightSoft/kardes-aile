import {DisasterVictimService} from '@disasterVictimListsModule/business/disaster-victim.service';
import {NavigationService} from '@appModule/services/navigation.service';
import {phoneValidator} from '@validationModule/phone-validator';
import {emailValidator} from '@validationModule/email-validator';
import {getValidationMessage} from '@validationModule/get-validation-message';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {ReactiveFormsModule, FormBuilder, Validators} from '@angular/forms';
import {Component, inject} from '@angular/core';
import {CommonModule} from '@angular/common';
import AddPageTitle from '@appModule/base-classes/add-page-title.abstract.class';
import {MatSelectModule} from '@angular/material/select';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import {AddChildComponent} from '../add-child/add-child.component';
import {MatTableModule} from '@angular/material/table'
import {CreateDisasterVictimModel} from '@appModule/models/disaster-victim/create-disaster-victim.model';
import {UpdateDisasterVictimModel} from '@appModule/models/disaster-victim/update-disaster-victim.model';
import {CreateChildModel} from '@appModule/models/child/create-child.model';
import {Genders} from '@appModule/models/shared/genders.enum';
import {AddressService} from '@appModule/services/address.service';
import {CountryResultModel} from '@appModule/models/country-result.model';
import {ChildResultModel} from '@appModule/models/child/child-result.model';
import {CityResultModel} from '@appModule/models/city-result.model';
import {ActivatedRoute} from '@angular/router';
import {Location} from '@angular/common';
import {ChildService} from '@appModule/services/child.service';
import {UpdateChildModel} from '@appModule/models/child/update-child.model';
import {Ng2TelInputModule} from "ng2-tel-input";
import {SnackbarService} from "@appModule/services/snackbar.service";

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
    MatTableModule,
    Ng2TelInputModule
  ],
  templateUrl: './add-disaster-victim.component.html',
  styleUrls: ['./add-disaster-victim.component.scss'],
  host: {
    class: 'row row-cols-2'
  }
})
export default class AddDisasterVictimComponent extends AddPageTitle {
  countryList$: CountryResultModel[];
  cityList$: CityResultModel[];
  tempCityList$: CityResultModel[];
  childrens: Array<CreateChildModel | UpdateChildModel> = [];
  displayedColumns: string[] = ['name', 'birthDate', 'gender', 'action'];
  disasterVictimId: string = undefined;
  childrensToBeDeleted: Array<CreateChildModel | UpdateChildModel> = [];
  userId?: string;
  private dialog = inject(MatDialog);
  private formBuilder = inject(FormBuilder);
  form = this.formBuilder.group({
    id: this.disasterVictimId,
    identityNumber: this.formBuilder.control(undefined, [
      Validators.required,
      Validators.min(9999999999),
      Validators.max(99999999999)
    ]),
    firstName: this.formBuilder.control(undefined, Validators.required),
    lastName: this.formBuilder.control(undefined, Validators.required),
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
    addressValidated: this.formBuilder.control(undefined),
  });

  constructor(private disasterVictimService: DisasterVictimService,
              private addressService: AddressService,
              private navigationService: NavigationService,
              private route: ActivatedRoute,
              private location: Location,
              private childService: ChildService,
              private snackbar: SnackbarService) {
    const id = route.snapshot.paramMap.get('id');
    super(id ? 'Afetzede G??ncelle' : 'Afetzede Kayit');
    if (id) {
      this.disasterVictimId = id;
      disasterVictimService.get(id).subscribe(result => {
        this.userId = result.userId;
        this.childService.list(result.userId).subscribe(childResult => {
          this.childrens = childResult;
        });
        this.addressService.cities(result.countryId).subscribe((cResult) => {
          console.log(result);
          this.cityList$ = cResult.sort((a, b) => a.name.localeCompare(b.name));
          this.form.patchValue(result);
          this.form.patchValue({firstName: `${result.firstName}`})
          this.form.patchValue({lastName: `${result.lastName}`})
        });

      });
    }
  }

  public get countries(): ReadonlyArray<CountryResultModel> {
    return this.countryList$;
  }

  public get tcknValidationMessage(): string {
    return getValidationMessage(this.form.controls.identityNumber);
  }

  public get firstNameValidationMessage(): string {
    return getValidationMessage(this.form.controls.firstName);
  }

  public get lastNameValidationMessage(): string {
    return getValidationMessage(this.form.controls.lastName);
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

  ngAfterViewInit() {
    this.addressService.countries().subscribe((result) => {
      this.countryList$ = result.sort((a, b) => a.name.localeCompare(b.name));
    });
  }

  public calculateAge(birthDate: Date): string {
    var timeDiff = Math.abs(Date.now() - new Date(birthDate).getTime());
    return Math.floor(timeDiff / (1000 * 3600 * 24) / 365.25).toString();
  }

  public getGender(gender: number): string {
    return Object.values(Genders)[gender].toString();
  }

  public showChildDialog(action: string, child: CreateChildModel | UpdateChildModel, index: number) {
    this.dialog.open(AddChildComponent, {data: {action, child}}).afterClosed().subscribe(result => {
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
    this.childrensToBeDeleted.push(child);
  }

  public updateChild(child: ChildResultModel) {
    const index = this.childrens.findIndex((el) => el === child);
    this.showChildDialog('Update', child, index);
  }

  public countryChanged(id: string) {
    this.addressService.cities(id).subscribe((result) => {
      this.cityList$ = result.sort((a, b) => a.name.localeCompare(b.name));
    });
  }

  submit() {
    console.log(this.form.value);
    this.childrens = this.childrens.map((el) => {
      el.gender = Number(el.gender);
      return el;
    });
    console.log(this.form.valid);
    if (this.form.valid) {
      if (!this.disasterVictimId) {

        if (this.childrens.length === 0) {
          this.snackbar.show('Warning', '??ocuk/lar eklenmeden kay??t yapamazs??n??z!');
          return;
        }

        const model = {
          firstName: this.form.value.firstName,
          lastName: this.form.value.lastName,
          email: this.form.value.email,
          phone: this.form.value.phone,
          address: this.form.value.address,
          cityId: this.form.value.cityId,
          countryId: this.form.value.countryId,
          temporaryAddress: this.form.value.temporaryAddress,
          temporaryCityId: this.form.value.temporaryCityId,
          identityNumber: this.form.value.identityNumber.toString(),
          children: this.childrens
        } as CreateDisasterVictimModel;
        this.disasterVictimService.create(model).subscribe(() => {
          this.snackbar.show('Success', 'Kayit eklendi');
          this.backToList();
        });
      } else {
        const updateModel = {
          id: this.disasterVictimId,
          firstName: this.form.value.firstName,
          lastName: this.form.value.lastName,
          email: this.form.value.email,
          phone: this.form.value.phone,
          address: this.form.value.address,
          cityId: this.form.value.cityId,
          countryId: this.form.value.countryId,
          temporaryAddress: this.form.value.temporaryAddress,
          temporaryCityId: this.form.value.temporaryCityId,
          identityNumber: this.form.value.identityNumber.toString(),
          status: 0,
        } as UpdateDisasterVictimModel;
        this.disasterVictimService.update(updateModel).subscribe(() => {
          this.childrensToBeDeleted.forEach(element => {
            if ('id' in element)
              this.childService.delete(element.id).subscribe((res) => {
              });
          });
          this.childrens.forEach(element => {
            console.log(element);
            setTimeout(() => {
              this.addOrUpdateChildren(element);
            }, 300);
          });
        }).add(() => {
          this.snackbar.show('Success', 'Kayit guncellendi');
          this.backToList();
        });
      }
    }
  }

  public addOrUpdateChildren(child: (UpdateChildModel | CreateChildModel)) {
    if ('id' in child) {
      this.childService.update({
        id: child.id,
        name: child.name,
        birthDate: child.birthDate,
        gender: child.gender
      }).subscribe((res) => {
        console.log('yey')
      });
    } else {
      this.childService.create({
        userId: this.userId,
        name: child.name,
        birthDate: child.birthDate,
        gender: child.gender
      }).subscribe((res) => {
        console.log('yey')
      });
    }
  }

  public backToList() {
    this.navigationService.navigate('/disaster-victim-list');
  }
}
