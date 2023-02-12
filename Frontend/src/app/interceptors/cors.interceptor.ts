import { HttpEvent, HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import {inject} from "@angular/core";
import {AuthenticationService} from "@appModule/services/authentication.service";

export function corsInterceptor(req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> {
  const authenticationService = inject(AuthenticationService);
  let newRequest = req.clone({
    url: req.url.startsWith('http') ? req.url : environment.baseUrl + '/' + req.url,
    setHeaders: {
      Authorization: `Bearer ${authenticationService.authenticationValue?.bearer}`
    },
  });

  return next(newRequest);
}
