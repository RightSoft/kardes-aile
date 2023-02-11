import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import BaseListComponent from "@appModule/base-classes/base-list-component.abstract.class";

@Component({
  selector: 'app-moderator-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './moderator-list.component.html',
  styleUrls: ['./moderator-list.component.scss']
})
export default class ModeratorListComponent extends BaseListComponent {
  constructor() {
    super('Moderatör Listesi');
  }
}
