import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCosmeticComponent } from './edit-cosmetic.component';

describe('EditBlogpostComponent', () => {
  let component: EditCosmeticComponent;
  let fixture: ComponentFixture<EditCosmeticComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditCosmeticComponent]
    });
    fixture = TestBed.createComponent(EditCosmeticComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
