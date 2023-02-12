import { HttpEvent, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { inject } from '@angular/core';
import { AuthenticationService } from '@appModule/services/authentication.service';

export function errorInterceptor(
  req: HttpRequest<any>,
  next: HttpHandlerFn
): Observable<HttpEvent<any>> {
  return next(req).pipe(
    catchError((error) => {
      const authenticationService = inject(AuthenticationService);
      if (error.status === 401) {
        authenticationService.logout();
        location.reload();
      }
      return throwError(error);
    })
  );
}
