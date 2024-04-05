import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { BigIpConfigComponent } from './bigIpConfig.component';

describe('BigIpConfig', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        BigIpConfigComponent
      ],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(BigIpConfigComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'poseidon-app'`, () => {
    const fixture = TestBed.createComponent(BigIpConfigComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('poseidon-app');
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(BigIpConfigComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Hello, poseidon-app');
  });
});
