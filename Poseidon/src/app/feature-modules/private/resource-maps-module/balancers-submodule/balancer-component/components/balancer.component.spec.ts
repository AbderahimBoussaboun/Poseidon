import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BalancerComponent } from './balancer.component';

describe('BalancerComponent', () => {
  let component: BalancerComponent;
  let fixture: ComponentFixture<BalancerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BalancerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BalancerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
