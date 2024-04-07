import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CosmeticDetailsComponent } from './cosmetic-details.component';

describe('BlogDetailsComponent', () => {
  let component: CosmeticDetailsComponent;
  let fixture: ComponentFixture<CosmeticDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CosmeticDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CosmeticDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
