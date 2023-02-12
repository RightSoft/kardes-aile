import { RouterModule } from '@angular/router';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppHeaderComponent } from '../app-header/app-header.component';
import { LeftAsideComponent } from '../left-aside/left-aside.component';
import { PreloaderComponent } from '@sharedComponents/preloader/components/preloader/preloader.component';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    LeftAsideComponent,
    AppHeaderComponent,
    PreloaderComponent
  ],
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export default class LayoutComponent {}
