import { pipe } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { ParamMap } from '@angular/router';

export function GetValueOrDefaultFromRoute<T>(parameterName: string, valueSelector: (parameterValue: string) => T, defaultValue: T) {
  return pipe(map<ParamMap, T>(_ => _.has(parameterName) ? valueSelector(_.get(parameterName)) : defaultValue), shareReplay<T>(1));
}
