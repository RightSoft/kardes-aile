import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { Component, EventEmitter, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '@sharedComponents/search/search.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-list-tool-box',
  standalone: true,
  imports: [
    CommonModule,
    SearchComponent,
    MatCheckboxModule,
    FormsModule,
    MatButtonModule,
  ],
  templateUrl: './list-tool-box.component.html',
  styleUrls: ['./list-tool-box.component.scss'],
  outputs: ['onSearch', 'onCheckboxChange', 'onAddNew'],
  inputs: ['buttonLabel', 'checkboxLabel', 'url'],
  host: {
    class: 'd-flex',
  },
})
export class ListToolBoxComponent {
  private router = inject(Router);
  private activatedRoute = inject(ActivatedRoute);
  buttonLabel: string = 'Yeni Ekle';
  checkboxLabel: string;
  url: string;
  private _checked: boolean;
  public get checked(): boolean {
    return this._checked;
  }
  public set checked(value: boolean) {
    this._checked = value;
    this.onCheckboxChange.emit(this.checked);
  }
  onSearch = new EventEmitter<string>();
  onCheckboxChange = new EventEmitter<boolean>();
  onAddNew = new EventEmitter<never>();
  handleNewButtonClick() {
    if (this.url) {
      this.router.navigate([this.url], {
        relativeTo: this.activatedRoute,
      });
    }
    if (this.onAddNew.observed) {
      this.onAddNew.emit();
    }
  }
}
