import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListToolBoxComponent } from '@sharedComponents/list-tool-box/list-tool-box.component';
import BaseListComponent from '@appModule/base-classes/base-list-component.abstract.class';

@Component({
  selector: 'app-disaster-victim-list',
  standalone: true,
  imports: [CommonModule, ListToolBoxComponent],
  templateUrl: './disaster-victim-list.component.html',
  styleUrls: ['./disaster-victim-list.component.scss']
})
export default class DisasterVictimListComponent extends BaseListComponent {
  constructor() {
    super('Afetzede Listesi');
  }
}
