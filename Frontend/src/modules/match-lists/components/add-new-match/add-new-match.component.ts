import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {MatDialogModule} from '@angular/material/dialog';
import {Component, EventEmitter, inject} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatAutocompleteModule, MatAutocompleteSelectedEvent} from '@angular/material/autocomplete';
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
export class AddNewMatchComponent {
  public genderLabels = GendersLabel;
  searchSupporterData: SearchSupporterModel = new SearchSupporterModel(1, 20);
  searchDisasterVictimData: DisasterVictimSearchRequestModel = new DisasterVictimSearchRequestModel(1, 20);
  voluntarilyList$: SupporterSearchResultModel[];
  disasterVictimList$: DisasterVictimSearchResultModel[];
  voluntarilyChildList$: ChildResultModel[];
  disasterVictimChildList$: ChildResultModel[];
  formBuilder = inject(FormBuilder);
  addMatchForm = this.formBuilder.group({
    selectedVoluntarily: this.formBuilder.control(null, Validators.required),
    voluntarilySearchKeyword: this.formBuilder.control('', Validators.required),
    selectedDisasterVictim: this.formBuilder.control(null, Validators.required),
    disasterVictimSearchKeyword: this.formBuilder.control('', Validators.required),
    voluntarilyChild: this.formBuilder.control('', Validators.required),
    disasterVictimChild: this.formBuilder.control('', Validators.required)
  });

  constructor(private voluntarilyService: VoluntarilyService, private disasterVictimService: DisasterVictimService) {
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
  }

  public voluntarilyChanged(value: any) {
    if (value) {
      this.addMatchForm.controls.selectedVoluntarily.setValue(value.source.value.supporterId);

      this.voluntarilyService
        .get(value.source.value.supporterId)
        .subscribe((result) => {
          this.voluntarilyChildList$ = result.children;
        });
    } else {
      this.voluntarilyChildList$ = [];
    }
  }

  public disasterVictimChanged(value: any) {
    if (value) {
      this.addMatchForm.controls.selectedDisasterVictim.setValue(value.source.value.id);

      this.disasterVictimService
        .get(value.source.value.id)
        .subscribe((result) => {
          this.disasterVictimChildList$ = result.children;
        });
    } else {
      this.disasterVictimChildList$ = [];
    }
  }

  public get voluntarilyValidationMessage(): string {
    return getValidationMessage(this.addMatchForm.controls.voluntarilySearchKeyword);
  }

  public get disasterVictimValidationMessage(): string {
    return getValidationMessage(this.addMatchForm.controls.disasterVictimSearchKeyword);
  }

  public get voluntarilyChildValidationMessage(): string {
    return getValidationMessage(this.addMatchForm.controls.voluntarilyChild);
  }

  public get disasterVictimChildValidationMessage(): string {
    return getValidationMessage(this.addMatchForm.controls.disasterVictimChild);
  }

  displayUser(user: any) {
    return user && user.firstName + ' ' + user.lastName;
  }

  displayChild(childResultModel: ChildResultModel) {
    return childResultModel && childResultModel.name + ' '
      + (this.genderLabels ? this.genderLabels.get(childResultModel.gender) + ' ' : '')
      + new Date(childResultModel.birthDate).toLocaleDateString();
  }
}
