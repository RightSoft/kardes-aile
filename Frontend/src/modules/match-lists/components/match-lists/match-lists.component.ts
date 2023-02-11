import { AddNewMatchComponent } from './../add-new-match/add-new-match.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { AppService } from '@appModule/business/app.service';
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchComponent } from '@sharedComponents/search/search.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';

@Component({
  selector: 'app-match-lists',
  standalone: true,
  imports: [
    CommonModule,
    SearchComponent,
    MatCheckboxModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
  ],
  templateUrl: './match-lists.component.html',
  styleUrls: ['./match-lists.component.scss'],
})
export default class MatchListsComponent {
  private appService = inject(AppService);
  private dialog = inject(MatDialog);
  private keyword: string;
  constructor() {
    this.appService.pageTitle$.next('Eşleşme Listeleri');
  }
  onSearch(keyword: string) {
    this.keyword = keyword;
  }
  addNewMatch() {
    this.dialog.open(AddNewMatchComponent);
  }
}
