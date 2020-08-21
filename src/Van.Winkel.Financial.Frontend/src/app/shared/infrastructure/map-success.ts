import { map } from 'rxjs/operators';
import { ApiError } from '../models';

export function mapSuccess<T>() {
    return map<T | ApiError, T>(res => res instanceof ApiError ? null : res);
}
