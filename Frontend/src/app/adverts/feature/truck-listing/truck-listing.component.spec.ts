import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TruckListingComponent } from './truck-listing.component';

describe('TruckListingComponent', () => {
  let component: TruckListingComponent;
  let fixture: ComponentFixture<TruckListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TruckListingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TruckListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
