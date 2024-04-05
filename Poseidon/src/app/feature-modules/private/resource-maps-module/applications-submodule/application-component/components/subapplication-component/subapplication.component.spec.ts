import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubapplicationComponent } from './subapplication.component';

describe('SubapplicationComponentComponent', () => {
  let component: SubapplicationComponent;
  let fixture: ComponentFixture<SubapplicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubapplicationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubapplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
