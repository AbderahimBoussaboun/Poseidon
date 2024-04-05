import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComponentTypeComponent } from './component-type.component';

describe('ComponentTypeComponent', () => {
  let component: ComponentTypeComponent;
  let fixture: ComponentFixture<ComponentTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ComponentTypeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ComponentTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
