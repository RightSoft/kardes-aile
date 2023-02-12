import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppService } from '@appModule/business/app.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.scss'],
  host: {
    class: 'd-block border-bottom mb-3'
  }
})
export class AppHeaderComponent {
  private appService = inject(AppService);
  title$ = this.appService.pageTitle$;
}
