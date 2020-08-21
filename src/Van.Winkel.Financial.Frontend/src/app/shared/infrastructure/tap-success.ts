import { tap } from 'rxjs/operators';
import { ApiError } from '../models';

export function tapSuccess<T>(fn: (p: T) => void) {
    return tap<T | ApiError>(res => res instanceof ApiError ? null : fn(res));
}
