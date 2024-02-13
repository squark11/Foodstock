import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleCoverComponent } from './role-cover.component';

describe('RoleCoverComponent', () => {
  let component: RoleCoverComponent;
  let fixture: ComponentFixture<RoleCoverComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RoleCoverComponent]
    });
    fixture = TestBed.createComponent(RoleCoverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
