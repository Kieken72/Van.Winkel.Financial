import { Observable, of } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiError } from '../../models';
import { catchError } from 'rxjs/operators';

function c<T>(err): Observable<T | ApiError | null> {
    if (err instanceof HttpErrorResponse) {
        if (err.status === 400) {
            return of(new ApiError(err.error));
        }
    }
    return of(null);
}

export function catchValidationBag<T>() {
    return catchError<T, Observable<T | ApiError | null>>(err => c<T>(err));
}
