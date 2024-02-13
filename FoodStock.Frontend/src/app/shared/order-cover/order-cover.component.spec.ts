import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderCoverComponent } from './order-cover.component';

describe('OrderCoverComponent', () => {
  let component: OrderCoverComponent;
  let fixture: ComponentFixture<OrderCoverComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OrderCoverComponent]
    });
    fixture = TestBed.createComponent(OrderCoverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
