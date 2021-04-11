import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { FormCustomComponent } from './form-custom.component';

describe('FormCustomComponent', () => {
  let component: FormCustomComponent;
  let fixture: ComponentFixture<FormCustomComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ FormCustomComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormCustomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
