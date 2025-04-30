import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../accounts/data-access/auth.service';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { ChatService } from '../chat/data-access/chat-service';

@Component({
  selector: 'app-header',
  imports: [CommonModule, RouterLink, NzIconModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  constructor(public authService: AuthService,
    private chatService: ChatService) {}

  messageClick() {
    this.chatService.setMode('list');
    this.chatService.loadUserDialogs();
    this.chatService.open();
  }
  
  logout() {

  }

}
