import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListToolBoxComponent } from '@sharedComponents/list-tool-box/list-tool-box.component';
import BaseListComponent from '@appModule/base-classes/base-list-component.abstract.class';

@Component({
  selector: 'app-voluntarily-list',
  standalone: true,
  imports: [CommonModule, ListToolBoxComponent],
  templateUrl: './voluntarily-list.component.html',
  styleUrls: ['./voluntarily-list.component.scss'],
})
export default class VoluntarilyListComponent extends BaseListComponent {
  constructor() {
    super('Gönüllü Listesi');
  }
}
