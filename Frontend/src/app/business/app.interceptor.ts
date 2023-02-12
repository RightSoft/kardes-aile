import { AuthenticationService } from '@appModule/services/authentication.service';
import { AppService } from '@appModule/business/app.service';
import { inject } from '@angular/core';
import { HttpHandlerFn, HttpHeaders, HttpRequest } from '@angular/common/http';
import { finalize } from 'rxjs';

const appInterceptor = (request: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const appService = inject(AppService);
  const authenticationService = inject(AuthenticationService);
  const token = authenticationService.authenticationValue.bearer;
  setTimeout(() => {
    appService.isLoading$.next(true);
  });

  const newRequest = request.clone({
    headers: new HttpHeaders({
      Authorization: `Bearer ${token}`
    })
  });
  return next(newRequest).pipe(
    finalize(() => {
      appService.isLoading$.next(false);
    })
  );
};

export default appInterceptor;
