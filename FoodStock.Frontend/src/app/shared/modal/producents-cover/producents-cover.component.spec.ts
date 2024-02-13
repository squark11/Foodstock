import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProducentsCoverComponent } from './producents-cover.component';

describe('ProducentsCoverComponent', () => {
  let component: ProducentsCoverComponent;
  let fixture: ComponentFixture<ProducentsCoverComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProducentsCoverComponent]
    });
    fixture = TestBed.createComponent(ProducentsCoverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
