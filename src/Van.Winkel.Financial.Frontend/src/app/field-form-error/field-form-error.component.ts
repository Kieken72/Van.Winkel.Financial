import { Component, Input, ElementRef, OnDestroy, AfterViewInit } from '@angular/core';
import { ApiError, ValidationErrorCode, Error, uniqueBy } from '../shared';
import { ReplaySubject, combineLatest, Observable, Subject, BehaviorSubject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import * as $ from 'jquery';

@Component({
  selector: 'app-field-form-error',
  templateUrl: './field-form-error.component.html',
  styleUrls: ['./field-form-error.component.css']
})
export class FieldFormErrorComponent implements OnDestroy, AfterViewInit {
  private destroy$ = new Subject();
  private apiError$ = new ReplaySubject<ApiError>(1);
  private errorCodes$ = new ReplaySubject<ValidationErrorCode[]>(1);
  private identifyByParameter$ = new BehaviorSubject<{ key: string, code?: keyof typeof ValidationErrorCode, value: any }>(null);
  errors$: Observable<Error[]>;

  ValidationErrorCode = ValidationErrorCode;

  @Input() set error(v: ApiError) {
    this.apiError$.next(v);
  }

  @Input() set possibleErrors(v: (keyof typeof ValidationErrorCode)[]) {
    this.errorCodes$.next(v.map(_ => ValidationErrorCode[_]));
  }

  @Input() set identifyByParameter(i: { key: string, code?: keyof typeof ValidationErrorCode, value: any }) {
    this.identifyByParameter$.next(i);
  }

  constructor(private elementRef: ElementRef) {
    this.errors$ = combineLatest([this.apiError$, this.errorCodes$, this.identifyByParameter$]).pipe(
      map(([apiError, errorCodes, identifyByParameter]) => {
        if (apiError && !apiError.isValid) {
          let filter = (e: Error) => errorCodes.includes(e.validationErrorCode);
          if (identifyByParameter) {
            if (identifyByParameter.code) {
              filter = (e: Error) => errorCodes.includes(e.validationErrorCode)
                && (ValidationErrorCode[identifyByParameter.code] !== e.validationErrorCode ||
                  e.namedParameters[identifyByParameter.key] === identifyByParameter.value);
            } else {
              filter = (e: Error) => errorCodes.includes(e.validationErrorCode)
                && e.namedParameters[identifyByParameter.key] === identifyByParameter.value;
            }
          }
          return apiError.errors.filter(filter).filter(uniqueBy('validationErrorCode'));
        }
        return [];
      })
    );


  }

  ngAfterViewInit() {
    this.errors$.pipe(takeUntil(this.destroy$)).subscribe(e => {
      if (e.length > 0) {
        $(this.elementRef.nativeElement).find('.form-control:not(:disabled)').addClass('is-invalid');
      } else {
        $(this.elementRef.nativeElement).find('.form-control:not(:disabled)').removeClass('is-invalid');
      }
    });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

}
