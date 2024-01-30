import { TestBed } from '@angular/core/testing';

import { AddLikeCommentsServiceService } from './add-like-comments.service.service';

describe('AddLikeCommentsServiceService', () => {
  let service: AddLikeCommentsServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddLikeCommentsServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
