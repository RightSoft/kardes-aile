import { AppService } from '@appModule/business/app.service';
import { inject } from '@angular/core';
import { HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { finalize } from 'rxjs';

const appInterceptor = (request: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const appService = inject(AppService);
  setTimeout(() => {
    appService.isLoading$.next(true);
  });
  return next(request).pipe(
    finalize(() => {
      appService.isLoading$.next(false);
    })
  );
};

export default appInterceptor;
