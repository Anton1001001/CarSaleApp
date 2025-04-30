import { Component, OnInit } from '@angular/core';
import { AdvertService } from '../../adverts/data-access/advert.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdvertListComponent } from "../../adverts/feature/advert-list/advert-list.component";
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzCarouselModule } from 'ng-zorro-antd/carousel';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzDividerModule } from 'ng-zorro-antd/divider';

@Component({
  selector: 'app-home',
  imports: [CommonModule, 
    FormsModule, 
    AdvertListComponent,
    ReactiveFormsModule,
    NzButtonModule,
    NzFormModule,
    NzInputModule,
    NzSelectModule,
    NzRadioModule,
    NzListModule,
    NzIconModule,
    NzTypographyModule,
    NzInputNumberModule,
    NzCarouselModule,
    NzCardModule,
    NzDividerModule
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  adverts: any[] = [];
  isLoading = true;

  popularBrands = [
    { name: 'BMW', logo: 'https://upload.wikimedia.org/wikipedia/commons/4/44/BMW.svg' },
    { name: 'Mercedes', logo: 'https://upload.wikimedia.org/wikipedia/commons/thumb/9/90/Mercedes-Logo.svg/1024px-Mercedes-Logo.svg.png' },
    { name: 'Audi', logo: 'https://upload.wikimedia.org/wikipedia/commons/thumb/9/92/Audi-Logo_2016.svg/1920px-Audi-Logo_2016.svg.png' },
    { name: 'Volkswagen', logo: 'https://upload.wikimedia.org/wikipedia/commons/thumb/6/6d/Volkswagen_logo_2019.svg/1024px-Volkswagen_logo_2019.svg.png' },
    { name: 'Toyota', logo: 'https://upload.wikimedia.org/wikipedia/commons/thumb/7/78/Toyota_Logo.svg/1280px-Toyota_Logo.svg.png' },
    { name: 'Kia', logo: 'https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/KIA_logo2.svg/1920px-KIA_logo2.svg.png' },
  ];
  

  constructor(private advertService: AdvertService, 
    private message: NzMessageService) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.advertService.getAdverts().subscribe({
      next: (adverts) => {
        this.adverts = adverts;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
        this.message.error('Ошибка при загрузке объявлений')
      }
    })
  }

  filters: any = {
    brand: null,
    model: null,
    generation: null,
    bodyType: null,
    fuelType: null,
    transmission: null,
    yearFrom: null,
    yearTo: null,
    priceFrom: null,
    priceTo: null
  };

  brands: string[] = ['Toyota', 'BMW', 'Mercedes', 'Audi'];
  modelsMap: { [brand: string]: string[] } = {
    Toyota: ['Camry', 'Corolla', 'RAV4'],
    BMW: ['3 Series', '5 Series', 'X5'],
    Mercedes: ['C-Class', 'E-Class', 'GLC'],
    Audi: ['A4', 'A6', 'Q5']
  };
  generationsMap: { [model: string]: string[] } = {
    Camry: ['XV40', 'XV50', 'XV70'],
    Corolla: ['E140', 'E170', 'E210'],
    RAV4: ['XA30', 'XA40', 'XA50'],
    '3 Series': ['E90', 'F30', 'G20'],
    '5 Series': ['E60', 'F10', 'G30'],
    X5: ['E70', 'F15', 'G05'],
    'C-Class': ['W204', 'W205', 'W206'],
    'E-Class': ['W211', 'W212', 'W213'],
    GLC: ['X253', 'X254'],
    A4: ['B8', 'B9'],
    A6: ['C6', 'C7', 'C8'],
    Q5: ['8R', 'FY']
  };

  models: string[] = [];
  generations: string[] = [];

  bodyTypes: string[] = ['Седан', 'Хэтчбек', 'Универсал', 'Внедорожник', 'Купе'];
  fuelTypes: string[] = ['Бензин', 'Дизель', 'Гибрид', 'Электро'];
  transmissions: string[] = ['Механика', 'Автомат', 'Робот', 'Вариатор'];

  loadAdverts(): void {

  }

  onBrandChange(brand: string): void {
    this.models = this.modelsMap[brand] || [];
    this.filters.model = null;
    this.generations = [];
    this.filters.generation = null;
  }

  onModelChange(model: string): void {
    this.generations = this.generationsMap[model] || [];
    this.filters.generation = null;
  }

  applyFilters(): void {
    console.log('Применены фильтры:', this.filters);
    this.loadAdverts();
  }
}
