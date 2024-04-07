import { TestBed } from '@angular/core/testing';

import { CosmeticService } from './cosmetic.service';

describe('CosmeticService', () => {
  let service: CosmeticService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CosmeticService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
