import {
    HttpErrorResponse,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
@Injectable({
    providedIn: 'root',
})
export class UnauthorizedInterceptorService implements HttpInterceptor {
    constructor(@Inject('BASE_API_URL') private baseUrl: string) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            tap(
                () => {
                    next.handle(request);
                },
                error => {
                    if (error instanceof HttpErrorResponse) {
                        if (error.status === 401 || error.status === 403) {
                            window.location.href = this.baseUrl + '/account/login';
                        }
                    }
                },
            ),
        );
    }
}
