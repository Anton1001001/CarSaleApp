import { Component, EventEmitter, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdvertService } from '../../data-access/advert.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CommonModule } from '@angular/common';
import { NzImageModule, NzImagePreviewComponent, NzImagePreviewRef, NzImageService } from 'ng-zorro-antd/image';
import { getFormattedFloat, getNumberRuFormat, getRelativeTime } from '../../utils/advert-utils';
import { take } from 'rxjs';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { ChatDrawerComponent } from "../../../chat/feature/chat-drawer/chat-drawer.component";
import { ChatService } from '../../../chat/data-access/chat-service';
import { AdvertPreviewResponse } from '../../../chat/data-access/models/models';
import { AuthService } from '../../../accounts/data-access/auth.service';

@Component({
  selector: 'app-advert-details',
  imports: [CommonModule, NzImageModule, NzIconModule, NzButtonModule, NzBreadCrumbModule, NzDrawerModule],
  templateUrl: './advert-details.component.html',
  styleUrl: './advert-details.component.css'
})
export class AdvertDetailsComponent implements OnInit {
  chatDrawerVisible = false;
  advert: any;
  selectedDialog: any;
  selectedAdvertPreview: AdvertPreviewResponse | null = null;
  selectedImage: any;
  images: any[] = [];

  cardTitle?: string;
  date?: string;
  id?: string;

  mileageKmString?: string;
  priceBynString?: string;
  priceUsdString?: string;
  engineCapacityString?: string;
  averageFuelConsumptionString?: string;

  engineCapacityValue = 2.0;
  enginePowerValue = 150;
  averageFuelConsumptionValue = 8.7;

  @Output() previewOpened = new EventEmitter<boolean>();
  isPreviewOpen = false;

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private router: Router,
    private advertService: AdvertService,
    private chatService: ChatService,
    private message: NzMessageService,
    private nzImageService: NzImageService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];
      if (id) 
        this.loadAdvert(id);
    });
  }

  private loadAdvert(id: number): void {
    this.advertService.getAdvertById(id).subscribe({
      next: (advert) => this.populateAdvertData(advert),
      error: () => this.message.error('Ошибка при загрузке информации об объявлении')
    });
  }

  private populateAdvertData(advert: any): void {
    this.advert = advert;

    const { brand, model, generation, year, mileageKm } = advert.parameters;
    const { byn, usd } = advert.price;

    this.cardTitle = `Продажа ${brand} ${model} ${generation}, ${year} г. ${advert.shortLocationName}`;
    this.date = `опубликовано ${getRelativeTime(advert.publishedAt)}`;
    this.id = `№ ${advert.id}`;

    this.images = advert.photos.map((photo: any, index: number) => ({
      id: index,
      src: photo.big.url,
      width: photo.big.width,
      height: photo.big.height
    }));

    this.selectedImage = this.images[0];

    this.mileageKmString = getNumberRuFormat(mileageKm);
    this.priceBynString = getNumberRuFormat(byn);
    this.priceUsdString = getNumberRuFormat(usd);
    this.engineCapacityString = getFormattedFloat(this.engineCapacityValue);
    this.averageFuelConsumptionString = getFormattedFloat(this.averageFuelConsumptionValue);
  }

  onClick(): void {
    this.previewOpened.emit(true);

    const previewRef = this.nzImageService.preview(this.images);
    previewRef.switchTo(this.selectedImage.id);

    const sub = previewRef.previewInstance.closeClick.subscribe(() => {
      this.previewOpened.emit(false);
      sub.unsubscribe();
    });
  }

  selectPhoto(image: any, thumb: HTMLElement): void {
    this.selectedImage = image;
    thumb.scrollIntoView({ behavior: 'smooth', inline: 'center', block: 'nearest' });
  }

  writeToSeller(advert: any): void {
    this.authService.user$.pipe(take(1)).subscribe(user => {
      if (!user) {
        this.router.navigate(['/login'], {
          queryParams: { returnUrl: this.router.url }
        });
        return;
      }
      this.chatService.setMode('dialog');
      this.chatService.open(); 
      this.chatService.checkDialog(advert.id).subscribe({
        next: (response) => {
          const dialogInfo = {
            advertInfo: response.advertInfo,
            dialogId: response.id,
            name: response.advertInfo.sellerName
          }
          this.chatService.setDialogInfo(dialogInfo);
        },
        error: (error) => {
          this.message.error(error.error.message);
        }
      });
    });
  }
  

  onChatDrawerClose(): void {
    this.chatDrawerVisible = false;
  }
}