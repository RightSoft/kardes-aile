import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { Observable } from 'rxjs';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { getValidationMessage } from '@validationModule/get-validation-message';

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
    ReactiveFormsModule,
  ],
  templateUrl: './add-new-match.component.html',
  styleUrls: ['./add-new-match.component.scss'],
})
export class AddNewMatchComponent {
  voluntarilyList$: Observable<any[]>;
  disasteVictimList$: Observable<any[]>;
  voluntarilyChildList$: Observable<any[]>;
  disasteVictimChildList$: Observable<any[]>;
  private formBuilder = inject(FormBuilder);
  addMatchForm = this.formBuilder.group({
    voluntarily: this.formBuilder.control('', Validators.required),
    disasteVictim: this.formBuilder.control('', Validators.required),
    voluntarilyChild: this.formBuilder.control('', Validators.required),
    disasteVictimChild: this.formBuilder.control('', Validators.required),
  });
  constructor() {}
  public get voluntarilyValidationMessage(): string {
    return getValidationMessage(this.addMatchForm.controls.voluntarily);
  }
  public get disasteVictimValidationMessage(): string {
    return getValidationMessage(this.addMatchForm.controls.disasteVictim);
  }
  public get voluntarilyChildValidationMessage(): string {
    return getValidationMessage(this.addMatchForm.controls.voluntarilyChild);
  }
  public get disasteVictimChildValidationMessage(): string {
    return getValidationMessage(this.addMatchForm.controls.disasteVictimChild);
  }
}
