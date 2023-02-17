import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandlerFn,
  HttpRequest
} from '@angular/common/http';
import { catchError, Observable, retry, throwError } from 'rxjs';
import { inject } from '@angular/core';
import { SnackbarService } from '@appModule/services/snackbar.service';
import { AuthenticationService } from '@appModule/services/authentication.service';

export function errorHandlerInterceptor(
  req: HttpRequest<any>,
  next: HttpHandlerFn
): Observable<HttpEvent<any>> {
  const snackbarService = inject(SnackbarService);
  const authenticationService = inject(AuthenticationService);
  return next(req).pipe(
    retry(0),
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        if (error.error) {
          snackbarService.show('Error', error.error);
        }
        authenticationService.logout();
        return throwError(error);
      }

      let errorMessage = '';
      if (error.error instanceof ErrorEvent) {
        // client-side error
        errorMessage = `Error: ${error.error.message}`;
      } else {
        // server-side error
        errorMessage = `Error Status: ${error.status}\nMessage: ${error.message}`;
        if (error.error?.error) {
          snackbarService.show('Error', error.error.error);
        } else {
          snackbarService.show('Error', error.message);
        }
      }
      console.log(errorMessage);
      return throwError(errorMessage);
    })
  );
}
