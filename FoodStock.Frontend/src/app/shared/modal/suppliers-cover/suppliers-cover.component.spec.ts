import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuppliersCoverComponent } from './suppliers-cover.component';

describe('SuppliersCoverComponent', () => {
  let component: SuppliersCoverComponent;
  let fixture: ComponentFixture<SuppliersCoverComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SuppliersCoverComponent]
    });
    fixture = TestBed.createComponent(SuppliersCoverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
