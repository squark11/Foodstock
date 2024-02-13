import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryCoverComponent } from './category-cover.component';

describe('CategoryCoverComponent', () => {
  let component: CategoryCoverComponent;
  let fixture: ComponentFixture<CategoryCoverComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CategoryCoverComponent]
    });
    fixture = TestBed.createComponent(CategoryCoverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
