import { HttpEvent, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export function corsInterceptor(
  req: HttpRequest<any>,
  next: HttpHandlerFn
): Observable<HttpEvent<any>> {
  const newRequest = req.clone({
    url:
      req.url.startsWith('http') || req.url.includes('assets/images')
        ? req.url
        : environment.baseUrl + '/' + req.url
  });

  return next(newRequest);
}
