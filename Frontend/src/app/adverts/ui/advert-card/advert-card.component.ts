import { CommonModule } from '@angular/common';
import { Component, Input, OnInit, SimpleChanges, OnChanges } from '@angular/core';
import { PhotoCarouselComponent } from "../photo-carousel/photo-carousel.component";
import { getFormattedFloat, getNumberRuFormat, getRelativeTime } from '../../utils/advert-utils';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-advert-card',
  standalone: true,
  imports: [CommonModule, RouterLink, PhotoCarouselComponent],
  templateUrl: './advert-card.component.html',
  styleUrl: './advert-card.component.css'
})
export class AdvertCardComponent implements OnInit {

  @Input() advert: any;

  engineCapacityValue: number = 2.0;
  engineCapacityString: string | undefined;

  mileageString: string | undefined;
  priceBynString: string | undefined;
  priceUsdString :string | undefined;
  date: string | undefined;

  ngOnInit(): void {
    const mileageValue: number = this.advert.parameters.mileageKm
    const priceBynValue: number = this.advert.price.byn;
    const priceUsdValue: number = this.advert.price.usd;
    this.mileageString = getNumberRuFormat(mileageValue);
    this.priceBynString = getNumberRuFormat(priceBynValue);
    this.priceUsdString = getNumberRuFormat(priceUsdValue);
    this.date = getRelativeTime(this.advert.publishedAt);
    this.engineCapacityString = getFormattedFloat(this.engineCapacityValue);
    console.log(this.advert);
  }

}
