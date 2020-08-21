import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FieldFormErrorComponent } from './field-form-error.component';

describe('FieldFormErrorComponent', () => {
  let component: FieldFormErrorComponent;
  let fixture: ComponentFixture<FieldFormErrorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FieldFormErrorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FieldFormErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
