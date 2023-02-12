import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot
} from '@angular/router';
import { NavigationService } from '@appModule/services/navigation.service';
import { AuthenticationService } from '@appModule/services/authentication.service';

@Injectable({ providedIn: 'root' })
export class AuthenticationGuard implements CanActivate {
  constructor(
    private navigationService: NavigationService,
    private authenticationService: AuthenticationService
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const authentication = this.authenticationService.authenticationValue;
    if (authentication) {
      return true;
    }

    this.navigationService.navigatToLogin({
      queryParams: {
        returnUrl: state.url
      }
    });

    return false;
  }
}
