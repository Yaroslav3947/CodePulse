import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CosmeticListComponent } from './cosmetic-list.component';

describe('BlogpostListComponent', () => {
  let component: CosmeticListComponent;
  let fixture: ComponentFixture<CosmeticListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CosmeticListComponent]
    });
    fixture = TestBed.createComponent(CosmeticListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
