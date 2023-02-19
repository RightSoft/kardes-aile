import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {MAT_DIALOG_DATA, MatDialogModule} from '@angular/material/dialog';
import {Component, Inject, inject, Optional} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {debounceTime} from 'rxjs';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {getValidationMessage} from '@validationModule/get-validation-message';
import {DisasterVictimService} from "@disasterVictimListsModule/business/disaster-victim.service";
import {VoluntarilyService} from "@voluntarilyListsModule/business/voluntarily.service";
import {SupporterSearchResultModel} from "@appModule/models/supporter/supporter-search-result.model";
import {SearchSupporterModel} from "@appModule/models/supporter/search-supporter.model";
import {DisasterVictimSearchResultModel} from "@appModule/models/disaster-victim/disaster-victim-search-result.model";
import {DisasterVictimSearchRequestModel} from "@appModule/models/disaster-victim/disaster-victim-search-request.model";
import {ChildResultModel} from "@appModule/models/child/child-result.model";
import {GendersLabel} from "@appModule/models/shared/genders.enum";
import {MatchListsService} from "@matchListsModule/business/match-lists.service";
import {SnackbarService} from "@appModule/services/snackbar.service";
import {NavigationService} from "@appModule/services/navigation.service";
import {CreateMatchModel} from "@appModule/models/match/create-match.model";
import {UpdateMatchModel} from "@appModule/models/match/update-match.model";

@Component({
  selector: 'app-add-new-match',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule,
    ReactiveFormsModule
  ],
  templateUrl: './add-new-match.component.html',
  styleUrls: ['./add-new-match.component.scss']
})
export default class AddNewMatchComponent {
  public genderLabels = GendersLabel;
  searchSupporterData: SearchSupporterModel = new SearchSupporterModel(1, 20);
  searchDisasterVictimData: DisasterVictimSearchRequestModel = new DisasterVictimSearchRequestModel(1, 20);
  voluntarilyList$: SupporterSearchResultModel[];
  disasterVictimList$: DisasterVictimSearchResultModel[];
  voluntarilyChildList$: ChildResultModel[];
  disasterVictimChildList$: ChildResultModel[];
  formBuilder = inject(FormBuilder);
  pageTitle$ = '';
  addMatchForm = this.formBuilder.group({
    matchId: this.formBuilder.control(null),
    voluntarilySearchKeyword: this.formBuilder.control(null),
    disasterVictimSearchKeyword: this.formBuilder.control(null),
    voluntarilyId: this.formBuilder.control(null, Validators.required),
    voluntarilyChildId: this.formBuilder.control('', Validators.required),
    voluntarilyChildSearchKeyword: this.formBuilder.control(null, Validators.required),
    victimId: this.formBuilder.control(null, Validators.required),
    disasterVictimChildId: this.formBuilder.control('', Validators.required),
    disasterVictimChildSearchKeyword: this.formBuilder.control(null, Validators.required)
  });

  constructor(@Optional() @Inject(MAT_DIALOG_DATA) public matchId: { id: string },
              private matchListsService: MatchListsService,
              private voluntarilyService: VoluntarilyService,
              private disasterVictimService: DisasterVictimService,
              private snackbar: SnackbarService,
              private navigationService: NavigationService) {
    this.pageTitle$ = matchId ? 'Eşleşme Güncelle' : 'Eşleşme Kayit';

    if (matchId) {
      this.matchListsService.get(matchId.id).subscribe(result => {
        this.addMatchForm.patchValue({
          disasterVictimSearchKeyword: {firstName: result.victimFirstName, lastName: result.victimLastName},
          disasterVictimChildSearchKeyword: {id: result.victimChildId, name: result.victimChildName},
          voluntarilySearchKeyword: {firstName: result.supporterFirstName, lastName: result.supporterLastName},
          voluntarilyChildSearchKeyword: {id: result.supporterChildId, name: result.supporterChildName},
          voluntarilyId: result.supporterId,
          victimId: result.victimId,
          disasterVictimChildId: result.victimChildId,
          voluntarilyChildId: result.supporterChildId,
          matchId: result.id,
        });
      })
    }

    this.addMatchForm.controls.voluntarilySearchKeyword.valueChanges
      .pipe(debounceTime(300))
      .subscribe((value) => {
        if (value && value.length > 2) {
          this.searchSupporterData.keyword = value;
          this.searchSupporterData.includeDeleted = false;

          this.voluntarilyService
            .search(this.searchSupporterData)
            .subscribe((result) => {
              this.voluntarilyList$ = result.list;
            });
        } else {
          this.voluntarilyList$ = [];
        }
      });

    this.addMatchForm.controls.disasterVictimSearchKeyword.valueChanges
      .pipe(debounceTime(300))
      .subscribe((value) => {
        if (value && value.length > 2) {
          this.searchDisasterVictimData.keyword = value;
          this.searchSupporterData.includeDeleted = false;

          this.disasterVictimService
            .search(this.searchDisasterVictimData)
            .subscribe((result) => {
              this.disasterVictimList$ = result.list;
            });
        } else {
          this.disasterVictimList$ = [];
        }
      });

    this.addMatchForm.controls.voluntarilyId.valueChanges
      .subscribe(value => {
        if (value) {
          this.voluntarilyService
            .get(value)
            .subscribe((result) => {
              this.voluntarilyChildList$ = result.children;
            })
        } else {
          this.voluntarilyChildList$ = [];
        }
      });

    this.addMatchForm.controls.victimId.valueChanges
      .subscribe(value => {
        if (value) {
          this.disasterVictimService
            .get(value)
            .subscribe((result) => {
              this.disasterVictimChildList$ = result.children;
            });
        } else {
          this.disasterVictimChildList$ = [];
        }
      });
  }

  public voluntarilyChanged(value: any) {
    if (value) {
      this.addMatchForm.controls.voluntarilyId.setValue(value.source.value.supporterId);
    }
  }

  public disasterVictimChanged(value: any) {
    if (value) {
      this.addMatchForm.controls.victimId.setValue(value.source.value.id);
    }
  }

  public voluntarilyChildChanged(value: any) {
    this.addMatchForm.controls.voluntarilyChildId.setValue(value.source.value.id);
  }

  public disasterVictimChildChanged(value: any) {
    this.addMatchForm.controls.disasterVictimChildId.setValue(value.source.value.id);
  }

  public get voluntarilyValidationMessage(): string {
    return getValidationMessage(this.addMatchForm.controls.voluntarilySearchKeyword);
  }

  public get disasterVictimValidationMessage(): string {
    return getValidationMessage(this.addMatchForm.controls.disasterVictimSearchKeyword);
  }

  public get voluntarilyChildValidationMessage(): string {
    return getValidationMessage(this.addMatchForm.controls.voluntarilyChildId);
  }

  public get disasterVictimChildValidationMessage(): string {
    return getValidationMessage(this.addMatchForm.controls.disasterVictimChildId);
  }

  displayUser(user: any) {
    return user && user.firstName + ' ' + user.lastName;
  }

  displayChild(childResultModel: ChildResultModel) {
    if (childResultModel) {
      return `${childResultModel.name}${(this.genderLabels && childResultModel.gender ? ' ' + this.genderLabels.get(childResultModel.gender) : '')}${(childResultModel.birthDate ? ' ' + new Date(childResultModel.birthDate).toLocaleDateString() : '')}`;
    } else {
      return '';
    }
  }

  onSave() {
    if (this.addMatchForm.valid) {
      if (!this.addMatchForm.value.matchId) {
        const model = {
          SupporterId: this.addMatchForm.value.voluntarilyId,
          SupporterChildId: this.addMatchForm.value.voluntarilyChildId,
          VictimId: this.addMatchForm.value.victimId,
          VictimChildId: this.addMatchForm.value.disasterVictimChildId
        } as CreateMatchModel;

        this.matchListsService.create(model).subscribe(() => {
          this.onCancel();
        }).add(() => this.snackbar.show('Success', 'Yeni kayıt eklendi'));
      } else {
        const updateModel = {
          MatchId: this.addMatchForm.value.matchId,
          SupporterId: this.addMatchForm.value.voluntarilyId,
          SupporterChildId: this.addMatchForm.value.voluntarilyChildId,
          VictimId: this.addMatchForm.value.victimId,
          VictimChildId: this.addMatchForm.value.disasterVictimChildId,
          Active: true
        } as UpdateMatchModel;

        this.matchListsService.update(updateModel).subscribe(() => {
          this.onCancel();
        }).add(() => this.snackbar.show('Success', 'Kayıt güncellendi'));
      }
    }
  }

  onCancel() {
    this.navigationService.navigate('/match');
  }
}
