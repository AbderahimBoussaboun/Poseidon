import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServerApplicationComponent } from './server-application.component';

describe('ServerApplicationComponent', () => {
  let component: ServerApplicationComponent;
  let fixture: ComponentFixture<ServerApplicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServerApplicationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ServerApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
