import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { CategoryResponse } from '../../data-access/models/category-response';
import { AdvertService } from '../../data-access/advert.service';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-new-ad',
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './new-ad.component.html',
  styleUrl: './new-ad.component.css'
})
export class NewAdComponent {
  categories: CategoryResponse[] = [];
  advertService: AdvertService = inject(AdvertService);
  selectedCategoryIndex: number | null = 0;

  selectCategory(index: number) {
    this.selectedCategoryIndex = this.selectedCategoryIndex === index ? null : index;
  }

  constructor() {
    this.advertService.getCategories().subscribe({
      next: (categories: CategoryResponse[]) => {
        this.categories = categories;
      }
    });
  }

}

