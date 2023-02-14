import {AfterViewInit, Component, inject} from '@angular/core';
import {CommonModule} from '@angular/common';
import {VoluntarilyService} from "@voluntarilyListsModule/business/voluntarily.service";
import AddPageTitle from "@appModule/base-classes/add-page-title.abstract.class";
import {MatInputModule} from "@angular/material/input";
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {getValidationMessage} from "@validationModule/get-validation-message";
import {atLeastOne} from "@validationModule/custom-validator";
import {emailValidator} from "@validationModule/email-validator";
import {phoneValidator} from "@validationModule/phone-validator";
import {MatTableDataSource, MatTableModule} from "@angular/material/table";
import {ChildResultModel} from "@appModule/models/child-result.model";
import {MatIconModule} from "@angular/material/icon";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatCardModule} from "@angular/material/card";
import {MatButtonModule} from "@angular/material/button";
import {CreateSupporterModel} from "@appModule/models/create-supporter.model";
import {NavigationService} from "@appModule/services/navigation.service";
import {ActivatedRoute} from "@angular/router";
import {UpdateSupporterModel} from "@appModule/models/update-supporter.model";
import {FlexModule} from "@angular/flex-layout";
import {CacheService} from "@appModule/services/cache.service";
import {CountryResultModel} from "@appModule/models/country-result.model";
import {CityResultModel} from "@appModule/models/city-result.model";
import {of, switchMap} from "rxjs";
import {map} from "rxjs/operators";
import {SnackbarService} from "@appModule/services/snackbar.service";

@Component({
  selector: 'app-add-voluntarily',
  standalone: true,
  imports: [CommonModule, MatInputModule, MatAutocompleteModule, ReactiveFormsModule, MatTableModule, MatIconModule, MatToolbarModule, MatCardModule, MatButtonModule, FlexModule],
  templateUrl: './add-voluntarily.component.html',
  styleUrls: ['./add-voluntarily.component.scss']
})
export default class AddVoluntarilyComponent extends AddPageTitle implements AfterViewInit {
  countryList: CountryResultModel[];
  cityList: CityResultModel[];
  dataSource: MatTableDataSource<ChildResultModel> = new MatTableDataSource<ChildResultModel>();
  displayedColumns: string[] = ['name', 'birthDate', 'gender', 'actions'];
  private formBuilder = inject(FormBuilder);
  addSupporterForm = this.formBuilder.group({
    supporterId: this.formBuilder.control(null),
    userId: this.formBuilder.control(null),
    firstName: this.formBuilder.control('', Validators.required),
    lastName: this.formBuilder.control('', Validators.required),
    emailOrPhoneInfo: new FormGroup({
      email: this.formBuilder.control(null, emailValidator),
      phone: this.formBuilder.control(null, phoneValidator),
    }, [atLeastOne(Validators.required, ['email', 'phone'])]),
    address: this.formBuilder.control(''),
    countryId: this.formBuilder.control(null),
    cityId: this.formBuilder.control(null),
    status: this.formBuilder.control(null)
  });

  constructor(private voluntarilyService: VoluntarilyService,
              private navigationService: NavigationService,
              private route: ActivatedRoute,
              private cacheService: CacheService,
              private snackbar: SnackbarService) {
    const id = route.snapshot.paramMap.get('id');
    super(id ? 'Gönüllü Güncelle' : 'Gönüllü Kayıt');

    this.cacheService.countries.subscribe(result => {
      this.countryList = result
    });

    if (id) {
      this.addSupporterForm.get('address').addValidators(Validators.required);
      voluntarilyService.get(id).pipe(
        switchMap(supporter => {
          if (supporter.countryId)
            return this.cacheService.getCities(supporter.countryId).pipe(map(cities => {
              this.cityList = cities;
              return supporter;
            }));
          return of(supporter);
        }),
      ).subscribe(result => {
        this.addSupporterForm.patchValue(result);
        this.addSupporterForm.patchValue({'emailOrPhoneInfo': {email: result.email, phone: result.phone}});
      })
    }
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

  ngAfterViewInit() {
    this.dataSource = new MatTableDataSource<ChildResultModel>([]);
  }

  onSave() {
    if (this.addSupporterForm.valid) {
      if (!this.addSupporterForm.value.supporterId) {
        const model = {
          firstName: this.addSupporterForm.value.firstName,
          lastName: this.addSupporterForm.value.lastName,
          email: this.addSupporterForm.value.emailOrPhoneInfo.email,
          phone: this.addSupporterForm.value.emailOrPhoneInfo.phone,
          address: this.addSupporterForm.value.address,
          cityId: this.addSupporterForm.value.cityId,
          countryId: this.addSupporterForm.value.countryId,
          children: []
        } as CreateSupporterModel;

        this.voluntarilyService.create(model).subscribe(() => {
          this.onCancel();
        }).add(() => this.snackbar.show('', 'Yeni kayıt eklendi'));
      } else {
        const updateModel = {
          userId: this.addSupporterForm.value.userId,
          firstName: this.addSupporterForm.value.firstName,
          lastName: this.addSupporterForm.value.lastName,
          email: this.addSupporterForm.value.emailOrPhoneInfo.email,
          phone: this.addSupporterForm.value.emailOrPhoneInfo.phone,
          address: this.addSupporterForm.value.address,
          cityId: this.addSupporterForm.value.cityId,
          countryId: this.addSupporterForm.value.countryId,
          status: this.addSupporterForm.value.status,
        } as UpdateSupporterModel;

        this.voluntarilyService.update(updateModel).subscribe(() => {
          this.onCancel();
        }).add(() => this.snackbar.show('', 'Kayıt güncellendi'));
      }
    }
  }

  onCancel() {
    this.navigationService.navigate('/voluntarily');
  }

  getCountryName(id: string): string {
    return this.countryList?.find(c => c.id === id)?.name;
  }

  countryChanged(value: string) {
    this.cacheService.getCities(value).subscribe(result => {
      this.cityList = result;
    });
  }

  getCityName(id: string): string {
    return this.cityList?.find(c => c.id === id)?.name;
  }
}
