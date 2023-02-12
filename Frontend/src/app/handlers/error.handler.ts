import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable } from '@angular/core';
import { SnackbarService } from '@appModule/services/snackbar.service';

@Injectable({
  providedIn: 'root'
})
export class AppErrorHandler implements ErrorHandler {
  constructor(private readonly snackbarService: SnackbarService) {}

  handleError(error: Error | HttpErrorResponse) {
    if (
      error instanceof HttpErrorResponse &&
      (<HttpErrorResponse>error).status !== 500 &&
      error.error
    ) {
      if (error.error.errors) {
        const validationError = error.error;
        for (const property in validationError.errors) {
          if (validationError.errors.hasOwnProperty(property)) {
            validationError.errors[property].map((item: string) => {
              this.snackbarService.show(validationError.title, item);
            });
          }
        }
      } else {
        this.snackbarService.show('Error', error.error);
      }
    } else {
      this.snackbarService.show('Error', 'Error occurred');
      console.log(error);
    }
  }
}
