import { map } from 'rxjs/operators';
import { ApiError } from '../models';

export function mapError<T>() {
    return map<T, ApiError>(res => res instanceof ApiError ? res : null);
}
