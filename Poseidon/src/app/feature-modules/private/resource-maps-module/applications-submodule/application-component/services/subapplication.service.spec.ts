import { TestBed } from '@angular/core/testing';

import { SubapplicationService } from './subapplication.service';

describe('SubapplicationService', () => {
  let service: SubapplicationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SubapplicationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
