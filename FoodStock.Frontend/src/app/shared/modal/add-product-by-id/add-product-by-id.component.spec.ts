import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProductByIdComponent } from './add-product-by-id.component';

describe('AddProductByIdComponent', () => {
  let component: AddProductByIdComponent;
  let fixture: ComponentFixture<AddProductByIdComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddProductByIdComponent]
    });
    fixture = TestBed.createComponent(AddProductByIdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
