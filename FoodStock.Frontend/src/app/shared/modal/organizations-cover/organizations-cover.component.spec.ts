import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationsCoverComponent } from './organizations-cover.component';

describe('OrganizationsCoverComponent', () => {
  let component: OrganizationsCoverComponent;
  let fixture: ComponentFixture<OrganizationsCoverComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OrganizationsCoverComponent]
    });
    fixture = TestBed.createComponent(OrganizationsCoverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
