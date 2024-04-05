import { TestBed } from '@angular/core/testing';

import { ServerApplicationService } from './server-application.service';

describe('ServerApplicationService', () => {
  let service: ServerApplicationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServerApplicationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
