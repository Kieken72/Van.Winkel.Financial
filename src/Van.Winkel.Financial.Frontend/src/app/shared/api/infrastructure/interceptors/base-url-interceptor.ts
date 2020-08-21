import { Injectable, Inject } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class BaseUrlInterceptor implements HttpInterceptor {

    constructor(
        @Inject('BASE_API_URL') private baseUrl: string) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // do not rewrite requests to assets
        if (request.url.startsWith('/performancetwin/assets')) {
            return next.handle(request);
        }
        if (request.url.startsWith('/flexmonster/')) {
            return next.handle(request);
        }
        const apiReq = request.clone({ url: `${this.baseUrl}/${request.url}` });
        return next.handle(apiReq);
    }
}
