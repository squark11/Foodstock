import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCoverComponent } from './product-cover.component';

describe('ProductCoverComponent', () => {
  let component: ProductCoverComponent;
  let fixture: ComponentFixture<ProductCoverComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductCoverComponent]
    });
    fixture = TestBed.createComponent(ProductCoverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
