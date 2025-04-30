import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';

import { NzCarouselModule } from 'ng-zorro-antd/carousel';

@Component({
  selector: 'app-photo-carousel',
  imports: [CommonModule, NzCarouselModule],
  templateUrl: './photo-carousel.component.html',
  styleUrls: ['./photo-carousel.component.css']
})
export class PhotoCarouselComponent implements OnInit {

  @Input() photos: any[] = [];

  ngOnInit(): void {

  }
  effect = 'scrollx';
}
