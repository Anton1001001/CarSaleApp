import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusListingComponent } from './bus-listing.component';

describe('BusListingComponent', () => {
  let component: BusListingComponent;
  let fixture: ComponentFixture<BusListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusListingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
