/* "Barrel" of Http Interceptors */
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { BaseUrlInterceptor } from './base-url-interceptor';
import { UnauthorizedInterceptorService } from './unauthorized-interceptor';
import { Provider } from '@angular/core';
import { UnhandledErrorInterceptor } from './unhandled-error-interceptor';

export const httpInterceptorProviders: Provider[] = [
    { provide: HTTP_INTERCEPTORS, useClass: UnhandledErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: BaseUrlInterceptor, multi: true },
    {
        provide: HTTP_INTERCEPTORS,
        useClass: UnauthorizedInterceptorService,
        multi: true,
    },
];
