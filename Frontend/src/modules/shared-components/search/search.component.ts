import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { Component, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
  outputs: ['onChange', 'onSearch'],
  inputs: ['label'],
})
export class SearchComponent {
  keyword: string;
  label: string = 'Ara...';
  onChange = new EventEmitter<string>();
  onSearch = new EventEmitter<string>();
  onChangeHandle() {
    this.onChange.emit(this.keyword);
  }
  onSearchHandle() {
    this.onSearch.emit(this.keyword);
  }
}
