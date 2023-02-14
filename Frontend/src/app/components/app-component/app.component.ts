import {RouterModule} from '@angular/router';
import {CommonModule} from '@angular/common';
import {Component} from '@angular/core';
import {MatNativeDateModule} from '@angular/material/core';
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  imports: [CommonModule, RouterModule, MatNativeDateModule],
})
export class AppComponent {
  title = 'kardes_aile';

  constructor(private matIconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    this.matIconRegistry
      .addSvgIcon('email-notvalidated', sanitizer
        .bypassSecurityTrustResourceUrl('../../../assets/images/icon/email-notvalidated.svg'))
      .addSvgIcon('email-validated', sanitizer
        .bypassSecurityTrustResourceUrl('../../../assets/images/icon/email-validated.svg'));
  }
}
