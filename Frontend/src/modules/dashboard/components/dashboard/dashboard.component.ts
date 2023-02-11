import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppService } from '@appModule/business/app.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export default class DashboardComponent {
  private appService = inject(AppService);
  constructor() {
    this.appService.pageTitle$.next('Dashboard');
  }
}
