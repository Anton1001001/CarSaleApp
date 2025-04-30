import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { AdvertCardComponent } from "../../ui/advert-card/advert-card.component";
import { Advert } from '../../data-access/advert.service';
import { getDeclension } from '../../utils/advert-utils';
import { DropdownEventComponent } from '../../../shared/components/dropdown-event/dropdown-event.component';


@Component({
  selector: 'app-advert-list',
  imports: [CommonModule, AdvertCardComponent, DropdownEventComponent],
  templateUrl: './advert-list.component.html',
  styleUrl: './advert-list.component.css'
})
export class AdvertListComponent implements OnInit, OnChanges {

  advertCountString: string | undefined;

  @Input() adverts: any[] = [];
  @Input() isLoading: boolean = true;

  constructor() {}

  ngOnInit(): void {
    console.log(this.adverts.length);
    // this.advertCountString = `Найдено ${getDeclension(this.adverts.length)}`;
  }
  
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['adverts']) {

      console.log('ngOnChanges adverts length:', this.adverts.length);
      this.advertCountString = `Найдено ${getDeclension(this.adverts.length)}`;
    }

  }
}
