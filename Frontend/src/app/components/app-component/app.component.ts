import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  imports: [CommonModule, RouterModule, MatNativeDateModule]
})
export class AppComponent {
  title = 'kardes_aile';

  // eslint-disable-next-line @typescript-eslint/no-empty-function
  constructor() {}
}
