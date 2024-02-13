import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProducentsComponent } from './producents.component';

describe('ProducentsComponent', () => {
  let component: ProducentsComponent;
  let fixture: ComponentFixture<ProducentsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProducentsComponent]
    });
    fixture = TestBed.createComponent(ProducentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
