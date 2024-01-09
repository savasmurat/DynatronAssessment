import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from './app/app.component';
import { APP_ROUTES } from './app/app.routes';
import { ErrorHandlerInterceptor } from './app/interceptors/error-handler-interceptor';


bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(HttpClientModule),
    provideRouter(APP_ROUTES,
      //withPreloading(PreloadAllModules), // withPreloading allows all lazy loading routes to preload
      //withDebugTracing() // withDebugTracing outputs to console diagnostic info regarding route navigation 
    ),
    importProvidersFrom(BrowserModule, NgbModule),
    ErrorHandlerInterceptor,
    {
      provide: HTTP_INTERCEPTORS,
      useExisting: ErrorHandlerInterceptor,
      multi: true
    }
  ]
})
  .catch(err => console.error(err));
