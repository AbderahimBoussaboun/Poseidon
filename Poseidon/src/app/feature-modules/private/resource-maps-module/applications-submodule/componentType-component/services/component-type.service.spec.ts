import { TestBed } from '@angular/core/testing';

import { ComponentTypeService } from './component-type.service';

describe('ComponentTypeService', () => {
  let service: ComponentTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ComponentTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
