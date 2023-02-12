import { AddNewMatchComponent } from './../add-new-match/add-new-match.component';
import { AppService } from '@appModule/business/app.service';
import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import BaseListComponent from '@appModule/base-classes/base-list-component.abstract.class';
import { ListToolBoxComponent } from '@sharedComponents/list-tool-box/list-tool-box.component';

@Component({
  selector: 'app-match-lists',
  standalone: true,
  imports: [CommonModule, MatDialogModule, ListToolBoxComponent],
  templateUrl: './match-lists.component.html',
  styleUrls: ['./match-lists.component.scss']
})
export default class MatchListsComponent extends BaseListComponent {
  private dialog = inject(MatDialog);
  constructor() {
    super('Eşleşme Listesi');
  }
  addNewMatch() {
    this.dialog.open(AddNewMatchComponent);
  }
}
