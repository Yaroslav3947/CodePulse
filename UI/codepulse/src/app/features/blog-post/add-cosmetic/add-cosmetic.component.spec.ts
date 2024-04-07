import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCosmeticComponent } from './add-cosmetic.component';

describe('AddBlogpostComponent', () => {
  let component: AddCosmeticComponent;
  let fixture: ComponentFixture<AddCosmeticComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddCosmeticComponent]
    });
    fixture = TestBed.createComponent(AddCosmeticComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
