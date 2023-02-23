import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import AddPageTitle from '@appModule/base-classes/add-page-title.abstract.class';

@Component({
  selector: 'app-supporter-persisted',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './supporter-persisted.component.html',
  styleUrls: ['./supporter-persisted.component.scss']
})
export default class SupporterPersistedComponent extends AddPageTitle{
  constructor() {
    super('Teşekkürler');
  }
}
