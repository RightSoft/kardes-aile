import { Component, inject, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import AddPageTitle from '@appModule/base-classes/add-page-title.abstract.class';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { getValidationMessage } from '@validationModule/get-validation-message';
import { atLeastOne } from '@validationModule/custom-validator';
import { emailValidator } from '@validationModule/email-validator';
import { phoneValidator } from '@validationModule/phone-validator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { CreateSupporterModel } from '@appModule/models/supporter/create-supporter.model';
import { NavigationService } from '@appModule/services/navigation.service';
import { ActivatedRoute } from '@angular/router';
import { FlexModule } from '@angular/flex-layout';
import { CacheService } from '@appModule/services/cache.service';
import { CountryResultModel } from '@appModule/models/country-result.model';
import { CityResultModel } from '@appModule/models/city-result.model';
import { catchError, filter, Observable, of, startWith, switchMap, tap } from 'rxjs';
import { map } from 'rxjs/operators';
import { SnackbarService } from '@appModule/services/snackbar.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { GendersLabel } from '@appModule/models/shared/genders.enum';
import { CreateChildModel } from '@appModule/models/child/create-child.model';
import {
  ConfirmationDialogComponent
} from '@sharedComponents/confirmation-dialog/components/confirmation-dialog.component';
import { AddChildComponent } from '@disasterVictimListsModule/components/add-child/add-child.component';
import { InputTextComponent } from '@sharedComponents/input/input-text/input-text.component';
import { InputSelectComponent } from '@sharedComponents/input/input-select/input-select.component';
import { Ng2TelInputModule } from 'ng2-tel-input';
import { environment } from '../../../../environments/environment';
import { NgxCaptchaModule } from 'ngx-captcha';
import { PublicSupporterService } from '@appModule/services/public-supporter.service';

@Component({
  selector: 'app-public-facing',
  standalone: true,
  imports: [CommonModule,
    MatInputModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    MatTableModule,
    MatIconModule,
    MatToolbarModule,
    MatCardModule,
    MatButtonModule,
    FlexModule,
    MatDialogModule,
    InputTextComponent,
    InputSelectComponent,
    Ng2TelInputModule,
    NgxCaptchaModule,
    FormsModule],
  templateUrl: './public-facing.component.html',
  styleUrls: ['./public-facing.component.scss']
})
export default class AddVoluntarilyComponent extends AddPageTitle {
  countryList: CountryResultModel[];
  filteredCountryList$: Observable<CountryResultModel[]>;
  cityList: CityResultModel[];
  filteredCityList$: Observable<CityResultModel[]>;
  children: CreateChildModel[];
  captchaKey: string = environment.recaptcha.siteKey;
  displayedColumns: string[] = ['name', 'birthDate', 'gender', 'actions'];
  private formBuilder = inject(FormBuilder);
  addSupporterForm = this.formBuilder.group({
    firstName: this.formBuilder.control('', Validators.required),
    lastName: this.formBuilder.control('', Validators.required),
    emailOrPhoneInfo: new FormGroup({
      email: this.formBuilder.control('', emailValidator),
      phone: this.formBuilder.control('', phoneValidator)
    }, [atLeastOne(Validators.required, ['email', 'phone'])]),
    address: this.formBuilder.control(''),
    countryId: this.formBuilder.control(null),
    cityId: this.formBuilder.control(null),
    recaptcha: ['', Validators.required]
  });

  @ViewChild(NgxCaptchaModule) captchaComponent: NgxCaptchaModule;

  constructor(
    private publicSupporterService: PublicSupporterService,
    private navigationService: NavigationService,
    private route: ActivatedRoute,
    private cacheService: CacheService,
    private snackbar: SnackbarService,
    private dialog: MatDialog
  ) {

    super('Gönüllü Kayıt');

    this.children = [];
    this.cacheService.countries.subscribe(result => {
      this.countryList = result;
    });

    this.filteredCountryList$ = this.addSupporterForm.controls.countryId.valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value?.name;
        return name ?
          this.countryList?.filter(c => c.name.toUpperCase().startsWith(name.toUpperCase()))
          : this.countryList?.slice();
      })
    );

    this.filteredCityList$ = this.addSupporterForm.controls.cityId.valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value?.name;
        return name ?
          this.cityList?.filter(c => c.name.toUpperCase().startsWith(name.toUpperCase()))
          : this.cityList?.slice();
      })
    );
  }

  public get firstNameValidationMessage(): string {
    return getValidationMessage(this.addSupporterForm.controls.firstName);
  }

  public get lastNameValidationMessage(): string {
    return getValidationMessage(this.addSupporterForm.controls.lastName);
  }

  public get emailValidationMessage(): string {
    return getValidationMessage(this.addSupporterForm.controls.emailOrPhoneInfo.controls.email);
  }

  public get phoneValidationMessage(): string {
    return getValidationMessage(this.addSupporterForm.controls.emailOrPhoneInfo.controls.phone);
  }

  public get addressValidationMessage(): string {
    return getValidationMessage(this.addSupporterForm.controls.address);
  }

  onCountrySelect(option: { name: string, id: string }) {
    this.addSupporterForm.controls.countryId.setValue(option.id);
    this.cacheService.getCities(option.id).subscribe(result => {
      this.cityList = result;
    });
  }

  onCitySelect(option: { name: string, id: string }) {
    this.addSupporterForm.controls.cityId.setValue(option.id);
  }

  onSave() {
    console.log(this.addSupporterForm);
    if (this.addSupporterForm.valid) {
      if (this.children.length === 0) {
        this.snackbar.show('Warning', 'Ailelerimizle bağ kurmamız için, lütfen en az bir çocuğunuzu kaydedin');
        return;
      }

      const model = {
        firstName: this.addSupporterForm.value.firstName,
        lastName: this.addSupporterForm.value.lastName,
        email: this.addSupporterForm.value.emailOrPhoneInfo.email,
        phone: this.addSupporterForm.value.emailOrPhoneInfo.phone,
        address: this.addSupporterForm.value.address,
        cityId: this.addSupporterForm.value.cityId,
        countryId: this.addSupporterForm.value.countryId,
        children: this.children,
        recaptchaToken: this.addSupporterForm.value.recaptcha
      } as CreateSupporterModel;

      this.publicSupporterService.create(model).subscribe(() => {
          this.snackbar.show('Success', 'Yeni kayıt eklendi');
          this.navigateToThanks();
        },
        err => {
          this.snackbar.show('Error', err.error);
        });
    }
  }

  navigateToThanks() {
    this.navigationService.navigate('thanks');
  }

  getCountryName(id: string): string {
    return this.countryList?.find(c => c.id === id)?.name;
  }

  getCityName(id: string): string {
    return this.cityList?.find(c => c.id === id)?.name;
  }

  addOrEditChild(action: string, child?: CreateChildModel, index?: number) {
    const dialogRef = this.dialog.open(AddChildComponent, {
      data: { action, child }
    }).afterClosed().subscribe(result => {
      if (!result) return;

      const childResult = { name: result.data.name, birthDate: result.data.birthDate, gender: result.data.gender };
      if (result.event == 'Add') {
        this.children = [...this.children, childResult];
      } else if (result.event == 'Update') {
        console.log(result.data);
        this.children[index] = childResult;
        this.children = [...this.children];
      }
    });
  }

  public showUpdateChild(child: CreateChildModel) {
    const index = this.children.findIndex((el) => el === child);
    console.log(index);
    this.addOrEditChild('Update', child, index);
  }

  onDeleteChild(child: CreateChildModel) {
    const index = this.children.indexOf(child);
    if (index >= 0) {
      this.children.splice(index, 1);
    }
  }

  getChildren() {
    return new MatTableDataSource<CreateChildModel>(this.children);
  }

  getAge(birthDate: Date): string {
    return Math.floor(Math.abs(Date.now() - new Date(birthDate).getTime()) / (1000 * 3600 * 24) / 365.25).toString();
  }

  getGendersLabel(gender: number): string {
    return GendersLabel.get(gender);
  }
}
