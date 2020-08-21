import {
    HttpInterceptor,
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpErrorResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { InternalServerErrorStoreService } from '../../../store/internal-server-error-store.service';
@Injectable({
    providedIn: 'root',
})
export class UnhandledErrorInterceptor implements HttpInterceptor {
    constructor(private internalServerErrorStoreService: InternalServerErrorStoreService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            catchError((error: HttpErrorResponse) => {
                if (error.status === 500) {
                    this.internalServerErrorStoreService.setInternalError();
                    return throwError({ error: 'Internal server error!' });
                } else {
                    return throwError(error);
                }
            }),
        );
    }
}
