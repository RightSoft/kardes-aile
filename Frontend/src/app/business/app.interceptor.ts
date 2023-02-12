import { AuthenticationService } from '@appModule/services/authentication.service';
import { AppService } from '@appModule/business/app.service';
import { inject } from '@angular/core';
import { HttpHandlerFn, HttpHeaders, HttpRequest } from '@angular/common/http';
import { finalize } from 'rxjs';

const appInterceptor = (request: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const appService = inject(AppService);
  const authenticationService = inject(AuthenticationService);
  const authentication = authenticationService.authenticationValue;
  setTimeout(() => {
    appService.isLoading$.next(true);
  });

  if (authentication && authentication.bearer) {
    const newRequest = request.clone({
      headers: new HttpHeaders({
        Authorization: `Bearer ${authentication.bearer}`
      })
    });

    return next(newRequest).pipe(
      finalize(() => {
        appService.isLoading$.next(false);
      })
    );
  }

  return next(request).pipe(
    finalize(() => {
      appService.isLoading$.next(false);
    })
  );
};

export default appInterceptor;
