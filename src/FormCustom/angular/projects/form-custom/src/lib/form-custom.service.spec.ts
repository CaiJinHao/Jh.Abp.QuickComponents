import { TestBed } from '@angular/core/testing';

import { FormCustomService } from './form-custom.service';

describe('FormCustomService', () => {
  let service: FormCustomService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormCustomService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
