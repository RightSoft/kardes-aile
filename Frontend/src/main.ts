import {
  BrowserAnimationsModule,
  NoopAnimationsModule
} from '@angular/platform-browser/animations';
import { bootstrapApplication } from '@angular/platform-browser';
import {
  enableProdMode,
  ErrorHandler,
  importProvidersFrom
} from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { AppComponent } from '@appModule/components/app-component/app.component';
import appInterceptor from '@appModule/business/app.interceptor';
import projectRoutes from '@appModule/routes';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { corsInterceptor } from '@appModule/interceptors/cors.interceptor';
import { environment } from './environments/environment';
import { AppErrorHandler } from '@appModule/handlers/error.handler';
import { MatSnackBarModule } from '@angular/material/snack-bar';

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
    { provide: ErrorHandler, useClass: AppErrorHandler },
    provideHttpClient(withInterceptors([appInterceptor, corsInterceptor])),
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: { appearance: 'outline' }
    }
  ]
});
