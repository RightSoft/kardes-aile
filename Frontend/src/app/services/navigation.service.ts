import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {
  constructor(private router: Router) {}

  public navigate(route: string, options?: any) {
    this.router.navigate([route], options);
  }

  public navigatToLogin(options?: any) {
    this.navigate('/auth', options);
  }
}
