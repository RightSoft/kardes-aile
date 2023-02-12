import { MatButtonModule } from '@angular/material/button';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthenticationService } from '@appModule/services/authentication.service';

@Component({
  selector: 'app-left-aside-header',
  standalone: true,
  imports: [CommonModule, MatButtonModule],
  templateUrl: './left-aside-header.component.html',
  styleUrls: ['./left-aside-header.component.scss']
})
export class LeftAsideHeaderComponent {
  constructor(private authenticationService: AuthenticationService) {}

  logout() {
    this.authenticationService.logout();
  }
}
