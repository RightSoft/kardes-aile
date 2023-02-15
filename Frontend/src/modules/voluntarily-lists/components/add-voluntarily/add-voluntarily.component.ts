import {Component, inject} from '@angular/core';
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
import {ChildResultModel} from "@appModule/models/child/child-result.model";
import {MatIconModule} from "@angular/material/icon";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatCardModule} from "@angular/material/card";
import {MatButtonModule} from "@angular/material/button";
import {CreateSupporterModel} from "@appModule/models/supporter/create-supporter.model";
import {NavigationService} from "@appModule/services/navigation.service";
import {ActivatedRoute} from "@angular/router";
import {UpdateSupporterModel} from "@appModule/models/supporter/update-supporter.model";
import {FlexModule} from "@angular/flex-layout";
import {CacheService} from "@appModule/services/cache.service";
import {CountryResultModel} from "@appModule/models/country-result.model";
import {CityResultModel} from "@appModule/models/city-result.model";
import {catchError, filter, Observable, of, startWith, switchMap, tap} from "rxjs";
import {map} from "rxjs/operators";
import {SnackbarService} from "@appModule/services/snackbar.service";
import {MatDialog, MatDialogModule} from "@angular/material/dialog";
import {
  AddVoluntarilyChildComponent
} from "@voluntarilyListsModule/components/add-voluntarily-child/add-voluntarily-child.component";
import {GendersLabel} from "@appModule/models/shared/genders.enum";
import {ChildService} from "@appModule/services/child.service";
import {CreateChildModel} from "@appModule/models/child/create-child.model";
import {UpdateChildModel} from "@appModule/models/child/update-child.model";
import {
  ConfirmationDialogComponent
} from "@sharedComponents/confirmation-dialog/components/confirmation-dialog.component";

@Component({
  selector: 'app-add-voluntarily',
  standalone: true,
  imports: [CommonModule, MatInputModule, MatAutocompleteModule, ReactiveFormsModule, MatTableModule, MatIconModule, MatToolbarModule, MatCardModule, MatButtonModule, FlexModule, MatDialogModule],
  templateUrl: './add-voluntarily.component.html',
  styleUrls: ['./add-voluntarily.component.scss']
})
export default class AddVoluntarilyComponent extends AddPageTitle {
  countryList: CountryResultModel[];
  filteredCountryList$: Observable<CountryResultModel[]>;
  cityList: CityResultModel[];
  filteredCityList$: Observable<CityResultModel[]>;
  children: ChildResultModel[];
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
              private snackbar: SnackbarService,
              private dialog: MatDialog,
              private childService: ChildService) {

    const id = route.snapshot.paramMap.get('id');
    super(id ? 'Gönüllü Güncelle' : 'Gönüllü Kayıt');

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
      }),
    );

    this.filteredCityList$ = this.addSupporterForm.controls.cityId.valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value?.name;
        return name ?
          this.cityList?.filter(c => c.name.toUpperCase().startsWith(name.toUpperCase()))
          : this.cityList?.slice();
      }),
    );

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
        this.children = result.children;
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
          children: this.children.map(child => ({name: child.name, birthDate: child.birthDate, gender: child.gender}))
        } as CreateSupporterModel;

        this.voluntarilyService.create(model).subscribe(() => {
          this.onCancel();
        }).add(() => this.snackbar.show('Success', 'Yeni kayıt eklendi'));
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
        }).add(() => this.snackbar.show('Success', 'Kayıt güncellendi'));
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

  addOrEditChild(child?: ChildResultModel) {
    const dialogRef = this.dialog.open(AddVoluntarilyChildComponent, {
      data: child ?? new ChildResultModel()
    });

    const subscribeDialog = dialogRef.componentInstance.onSubmitChildEvent.subscribe((child: ChildResultModel) => {
      if (this.addSupporterForm.value.userId) {
        if (child.id) {
          this.childService.update({
            id: child.id,
            name: child.name,
            birthDate: child.birthDate,
            gender: child.gender
          } as UpdateChildModel).subscribe(_ => {
            this.refreshChildren();
            this.snackbar.show('Success', 'Çocuk kaydı güncellendi.');
          });
        } else {
          this.childService.create({
            userId: this.addSupporterForm.value.userId,
            name: child.name,
            birthDate: child.birthDate,
            gender: child.gender
          } as CreateChildModel).subscribe(_ => {
            this.refreshChildren();
            this.snackbar.show('Success', 'Yeni çocuk eklendi.');
          });
        }
      } else {
        this.children.push(child);
      }
    });

    dialogRef.afterClosed().subscribe(_ => {
      subscribeDialog.unsubscribe();
    });
  }

  onDeleteChild(id: string) {
    if (id) {
      this.dialog
        .open(ConfirmationDialogComponent, {
          width: '300px',
          data: {
            title: 'Uyarı',
            message: 'Çocuğu silmek istediğinize emin misiniz?'
          }
        })
        .afterClosed()
        .pipe(
          filter((result) => {
            return result;
          }),
          switchMap(() => this.childService.delete(id)),
          tap(() => this.snackbar.show('Success', 'Successfully deleted')),
          tap(() => this.refreshChildren()),
          catchError((error) => {
            return of(error);
          })
        )
        .subscribe((data) => {
          if (data && data.ok === false) {
            this.snackbar.show('Error', data.message);
          }
        });
    } else {
      this.children = this.children.filter(child => child.id != id);
    }
  }


  refreshChildren() {
    this.childService.list(this.addSupporterForm.value.userId).subscribe(result => {
      this.children = result;
    });
  }

  getChildren() {
    return new MatTableDataSource<ChildResultModel>(this.children);
  }

  getAge(birthDate: Date): string {
    return Math.floor(Math.abs(Date.now() - new Date(birthDate).getTime()) / (1000 * 3600 * 24) / 365.25).toString();
  }

  getGendersLabel(gender: number): string {
    return GendersLabel.get(gender);
  }
}
