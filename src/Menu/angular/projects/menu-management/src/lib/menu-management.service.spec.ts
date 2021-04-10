import { TestBed } from '@angular/core/testing';

import { MenuManagementService } from './menu-management.service';

describe('MenuManagementService', () => {
  let service: MenuManagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MenuManagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
