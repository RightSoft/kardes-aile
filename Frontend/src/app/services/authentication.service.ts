import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthenticationResponse } from '@appModule/models/auth/authentication-response';
import { AuthenticationRequest } from '@appModule/models/auth/authentication-request';
import { NavigationService } from '@appModule/services/navigation.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private readonly storageKey = 'authentication';
  private readonly authenticationSubject: BehaviorSubject<AuthenticationResponse>;
  public readonly authentication: Observable<AuthenticationResponse>;

  constructor(
    private httpClient: HttpClient,
    private navigationService: NavigationService
  ) {
    this.authenticationSubject = new BehaviorSubject<AuthenticationResponse>(
      JSON.parse(localStorage.getItem(this.storageKey))
    );
    this.authentication = this.authenticationSubject.asObservable();
  }

  public get authenticationValue(): AuthenticationResponse {
    return this.authenticationSubject.value;
  }

  public login(model: AuthenticationRequest) {
    return this.httpClient
      .post<AuthenticationResponse>('api/authentication/authenticate', model)
      .pipe(
        map((result) => {
          if (result && result.bearer) {
            localStorage.setItem(this.storageKey, JSON.stringify(result));
            this.authenticationSubject.next(result);
          }
          return result;
        })
      );
  }

  public logout() {
    localStorage.removeItem(this.storageKey);
    this.authenticationSubject.next(null);
    this.navigationService.navigatToLogin();
  }
}
