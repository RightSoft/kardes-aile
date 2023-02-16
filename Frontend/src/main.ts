import {
  BrowserAnimationsModule,
  NoopAnimationsModule
} from '@angular/platform-browser/animations';
import { bootstrapApplication } from '@angular/platform-browser';
import { enableProdMode, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { AppComponent } from '@appModule/components/app-component/app.component';
import appInterceptor from '@appModule/business/app.interceptor';
import projectRoutes from '@appModule/routes';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { corsInterceptor } from '@appModule/interceptors/cors.interceptor';
import { environment } from './environments/environment';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { errorInterceptor } from '@appModule/interceptors/error.interceptor';
import { errorHandlerInterceptor } from '@appModule/interceptors/error.handler.interceptor';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';

if (environment.production) {
  enableProdMode();
}

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(
      [BrowserAnimationsModule],
      NoopAnimationsModule,
      MatSnackBarModule
    ),
    provideRouter(projectRoutes),
    provideHttpClient(
      withInterceptors([
        appInterceptor,
        corsInterceptor,
        errorInterceptor,
        errorHandlerInterceptor
      ])
    ),
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: { appearance: 'outline' }
    },
    { provide: LocationStrategy, useClass: HashLocationStrategy }
  ]
});
