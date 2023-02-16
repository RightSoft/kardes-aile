import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandlerFn,
  HttpRequest
} from '@angular/common/http';
import { catchError, Observable, retry, throwError } from 'rxjs';
import { inject } from '@angular/core';
import { SnackbarService } from '@appModule/services/snackbar.service';

export function errorHandlerInterceptor(
  req: HttpRequest<any>,
  next: HttpHandlerFn
): Observable<HttpEvent<any>> {
  const snackbarService = inject(SnackbarService);
  return next(req).pipe(
    retry(0),
    catchError((error: HttpErrorResponse) => {
      let errorMessage = '';
      if (error.error instanceof ErrorEvent) {
        // client-side error
        errorMessage = `Error: ${error.error.message}`;
      } else {
        // server-side error
        errorMessage = `Error Status: ${error.status}\nMessage: ${error.message}`;
        snackbarService.show('Error', error.error?.error);
      }
      console.log(errorMessage);
      return throwError(errorMessage);
    })
  );
}
