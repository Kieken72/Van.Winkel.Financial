import { mergeMap, map, shareReplay, filter, startWith, distinctUntilChanged, debounce } from 'rxjs/operators';
import { Observable, pipe, of, merge, timer } from 'rxjs';

export interface Loading<T> {
  loaded: boolean;
  value: T;
}

export function mapLoading<T, U>(fn: (p: T) => Observable<U>) {
  return pipe(
    mergeMap<T, Observable<Loading<U>>>(_ => merge<Loading<U>, Loading<U>>(
      of({ loaded: false, value: null } as Loading<U>),
      fn(_).pipe(
        map(res => ({ loaded: true, value: res }))
      ))),
    shareReplay<Loading<U>>(1));
}

export function liftValue<T>() {
  return pipe(map<Loading<T>, T>(_ => _.value), filter(_ => !!_));
}

export function liftLoaded<T>(loadingAfter = 150) {
  return pipe(
    startWith(({ loaded: true } as Loading<T>)),
    map<Loading<T>, boolean>(_ => _.loaded),
    debounce(_ => timer(_ ? 0 : loadingAfter)),
    distinctUntilChanged());
}
