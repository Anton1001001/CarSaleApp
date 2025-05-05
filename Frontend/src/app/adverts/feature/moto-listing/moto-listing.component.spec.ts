import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MotoListingComponent } from './moto-listing.component';

describe('MotoListingComponent', () => {
  let component: MotoListingComponent;
  let fixture: ComponentFixture<MotoListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MotoListingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MotoListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
