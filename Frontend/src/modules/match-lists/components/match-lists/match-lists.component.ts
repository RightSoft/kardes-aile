import { AppService } from '@appModule/business/app.service';
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-match-lists',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './match-lists.component.html',
  styleUrls: ['./match-lists.component.scss'],
})
export default class MatchListsComponent {
  private appService = inject(AppService);
  constructor() {
    this.appService.pageTitle$.next('Eşleşme Listeleri');
  }
}
