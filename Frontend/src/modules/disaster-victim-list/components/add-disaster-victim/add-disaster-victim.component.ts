import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import AddPageTitle from '@appModule/base-classes/add-page-title.abstract.class';

@Component({
  selector: 'app-add-disaster-victim',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './add-disaster-victim.component.html',
  styleUrls: ['./add-disaster-victim.component.scss']
})
export default class AddDisasterVictimComponent extends AddPageTitle {
  constructor() {
    super('Afetzede Ekle');
  }
}
