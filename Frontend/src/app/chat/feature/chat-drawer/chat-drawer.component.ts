import { CommonModule } from '@angular/common';
import { AfterViewChecked, Component, ElementRef, HostListener, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { ChatService, DialogInfo, Message } from '../../data-access/chat-service';
import { GetUserDialogsResponse } from '../../data-access/models/models';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { NzIconModule } from 'ng-zorro-antd/icon';

@Component({
  selector: 'app-chat-drawer',
  standalone: true,
  imports: [CommonModule, NzButtonModule, NzDrawerModule, FormsModule, NzTabsModule, NzIconModule],
  templateUrl: './chat-drawer.component.html',
  styleUrl: './chat-drawer.component.css'
})
export class ChatDrawerComponent implements OnInit, OnDestroy, AfterViewChecked {
  @ViewChild('chatMessages') private chatMessages!: ElementRef;

  @Input() visible: boolean = false;

  mode: 'list' | 'dialog' = 'list';
  dialogs: GetUserDialogsResponse[] = [];
  dialogInfo: DialogInfo | null = null;
  messages: Message[] = [];

  private subscription = new Subscription();

  contextMenuVisible = false;
  contextMenuPosition = { x: 0, y: 0 };
  selectedMessage: Message | null = null;

  isEditing = false;
  editingMessageId: number | null = null;
  inputText = '';

  constructor(private chatService: ChatService, private router: Router) {
    console.log('chat-drawer constructor');
  }

  async ngOnInit(): Promise<void> {
    console.log('chat-drawer ngOnInit');

    await this.chatService.initSignalRConnection();

    this.subscription.add(this.chatService.dialogs$.subscribe(dialogs => {
      this.dialogs = dialogs;
    }));

    this.subscription.add(this.chatService.dialogInfo$.subscribe(dialogInfo => {
      this.dialogInfo = dialogInfo;
    }));

    this.subscription.add(this.chatService.mode$.subscribe(mode => {
      this.mode = mode;
    }));

    this.subscription.add(this.chatService.messages$.subscribe(messages => {
      this.messages = messages;
    }));
  }

  sendMessage(textInput: HTMLInputElement) {
    const text = this.inputText.trim();
    if (!text) return;

    if (this.isEditing && this.editingMessageId !== null) {
      const index = this.messages.findIndex(m => m.messageId === this.editingMessageId);
      if (index !== -1) {
        this.messages[index].text = text;
      }
      this.isEditing = false;
      this.editingMessageId = null;
    } else {
      if (!this.dialogInfo?.dialogId) {
        this.chatService.createDialog(this.dialogInfo?.advertInfo?.id, text);
        return;
      }
      this.chatService.createMessage(text, this.dialogInfo.dialogId).subscribe();
    }

    this.inputText = '';
    textInput.value = '';
  }

  onInputChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    this.inputText = target.value ?? '';
  }
  

  async openDialog(dialog: GetUserDialogsResponse) {
    await this.chatService.initSignalRConnection();
    this.chatService.setMode('dialog');

    this.chatService.setDialogInfo({
      advertInfo: dialog.advertInfo,
      dialogId: dialog.dialogId,
      name: dialog.isAdvertOwner ? dialog.name : dialog.advertInfo.sellerName
    });

    console.log(this.dialogInfo);
  }

  onMessageRightClick(event: MouseEvent, message: Message): void {
    event.preventDefault();
    this.selectedMessage = message;

    this.contextMenuPosition = { x: event.clientX, y: event.clientY };
    this.contextMenuVisible = true;
  }

  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent) {
    const target = event.target as HTMLElement;
    if (!target.closest('.message-context-menu')) {
      this.contextMenuVisible = false;
    }
  }

  editMessage() {
    if (!this.selectedMessage) return;

    this.isEditing = true;
    this.editingMessageId = this.selectedMessage.messageId;
    this.inputText = this.selectedMessage.text || '';
    this.contextMenuVisible = false;
  }

  deleteMessage() {
    console.log('Удалить:', this.selectedMessage);
    this.contextMenuVisible = false;
  }

  backToList() {
    this.chatService.setMode('list');
    this.chatService.loadUserDialogs();

    this.chatService.stopConnection();
  }

  goToAdvert(id: number | undefined) {
    this.close();
    this.router.navigate(['/advert-details', id]);
  }

  close() {
    this.chatService.close();
  }

  ngAfterViewChecked() {
    if (this.chatMessages) {
      this.scrollToBottom();
    }
  }

  scrollToBottom() {
    const el = this.chatMessages?.nativeElement;
    if (el) {
      el.scrollTop = el.scrollHeight;
    }
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
