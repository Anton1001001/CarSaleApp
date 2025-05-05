import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { HeaderComponent } from "./header/header.component";
import { AuthService } from './accounts/data-access/auth.service';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { AuthDrawerComponent } from "./accounts/feature/auth-drawer/auth-drawer.component";
import { FooterComponent } from "./footer/footer.component";
import { HomeComponent } from "./home/feature/home.component";
import { AdvertDetailsComponent } from './adverts/feature/advert-details/advert-details.component';
import { ChatDrawerComponent } from "./chat/feature/chat-drawer/chat-drawer.component";
import { ChatService } from './chat/data-access/chat-service';
import { Subscriber, Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [CommonModule, 
    RouterOutlet, 
    HeaderComponent, 
    NzDrawerModule, 
    FooterComponent,
    NzDrawerModule, 
    ChatDrawerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'Frontend';

  subscription = new Subscription();
  visible = false;

  constructor(private authService: AuthService,
    private chatService: ChatService) {}

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.subscription = this.chatService.visible$.subscribe(visible => {
      this.visible = visible
    })
    this.authService.loadUserFromStorage();
  }

  isPreviewOpen = false;

  onRouteActivate(component: any) {
    if (component && component instanceof AdvertDetailsComponent) {
      component.previewOpened.subscribe((value: boolean) => {
        this.isPreviewOpen = value;
      });
    }
  }

}
