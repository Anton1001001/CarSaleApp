import { Injectable, OnDestroy, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject, filter, first, Observable, ReplaySubject, Subject, take } from 'rxjs';
import { AdvertPreviewResponse, CheckDialogResponse, GetUserDialogsResponse } from './models/models';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../accounts/data-access/auth.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { environment } from '../../../environments/environment.development';

export interface Message {
  messageId: number;
  sentAt: Date;
  isIncoming: boolean;
  text: string;
}

export interface HubMessage {
  messageId: number;
  sentAt: Date;
  senderId: string;
  text: string;
}

export interface DialogInfo {
  advertInfo: AdvertPreviewResponse | null
  dialogId: number | null
  name: string | null
}

@Injectable({
  providedIn: 'root'
})
export class ChatService implements OnDestroy {
  private visibleSubject = new BehaviorSubject<boolean>(false);
  visible$ = this.visibleSubject.asObservable();

  private dialogInfoSubject = new ReplaySubject<DialogInfo | null>(1);
  public dialogInfo$ = this.dialogInfoSubject.asObservable();

  private dialogsSubject = new ReplaySubject<GetUserDialogsResponse[]>(1);
  dialogs$ = this.dialogsSubject.asObservable();
  
  private messagesSubject = new ReplaySubject<Message[]>(1);
  messages$ = this.messagesSubject.asObservable();

  private isConnectionStartedSubject = new BehaviorSubject<boolean>(false);
  isConnectionStarted$ = this.isConnectionStartedSubject.asObservable();

  private modeSubject = new ReplaySubject<'list' | 'dialog'>(1);
  mode$ = this.modeSubject.asObservable();

  private hubConnection!: HubConnection;

  constructor(private http: HttpClient, private authService: AuthService, private message: NzMessageService) {}

  async initSignalRConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${environment.appUrl}/chatHub`)
      .build();

    await this.startConnection();
    this.hubConnection.on('ReceiveMessage', (response) => {
      const hubMessage = response.value;
      const message: Message = {
        messageId: hubMessage.messageId,
        sentAt: hubMessage.sentAt,
        isIncoming: hubMessage.senderId === this.authService.user?.userId,
        text: hubMessage.text
      };
      this.addMessageToList(message);
    });
  }

  private addMessageToList(message: Message): void {
    this.messagesSubject.pipe(take(1)).subscribe(currentMessages => {
      this.messagesSubject.next([...currentMessages, message]);
    })
  }

  private async startConnection(): Promise<void> {

      try {
        console.log('connection to signalR started');
        await this.hubConnection.start();
        this.isConnectionStartedSubject.next(true);
        console.log('SignalR connection established');
      } catch (err) {
        console.error('SignalR connection error: ', err);
        this.isConnectionStartedSubject.next(false);
        setTimeout(() => {
          this.startConnection();
        }, 5000);
      }

  }

  private waitForConnection() {
    return this.isConnectionStarted$.pipe(
      filter((started) => started),
      first()
    )
  }
  
  setDialogInfo(dialogInfo: DialogInfo) {
    this.dialogInfoSubject.next(dialogInfo);
    console.log(dialogInfo);
    if (dialogInfo.dialogId) {
      this.joinGroup(dialogInfo.dialogId);
      this.getDialogMessages(dialogInfo.dialogId);
    }
  }

  setMode(mode: 'list' | 'dialog') {
    this.modeSubject.next(mode);
  }

  public async joinGroup(dialogId: number): Promise<void> {
    console.log('joinGroup entered')
    console.log('waiting for connection')
    this.waitForConnection().subscribe((flag) => {
      console.log('connected to sig, now join' + flag)
      this.hubConnection.invoke('JoinGroup', dialogId)
      .catch(err => {
        console.error('Error joining group: ', err)
        console.log(this.hubConnection.state)

      });
    })
  }
  
  open() {
    this.visibleSubject.next(true);
  }

  close() {
    this.stopConnection();
    this.visibleSubject.next(false);
    this.messagesSubject.next([]);
    this.dialogInfoSubject.next(null)
    this.dialogsSubject.next([]);

  }

  checkDialog(advertId: number) {
    return this.http.get<CheckDialogResponse>(`${environment.appUrl}/api/chat/dialogs/check/${advertId}`);
  }

  getUserDialogs(): Observable<GetUserDialogsResponse[]> {
    return this.http.get<GetUserDialogsResponse[]>(`${environment.appUrl}/api/chat/dialogs`);
  }

  loadUserDialogs(): void {
    this.getUserDialogs().subscribe({
      next: (dialogs) => {
        this.dialogsSubject.next(dialogs);
        console.log(dialogs);
      },
      error: (err) => {
        console.error('Ошибка при загрузке диалогов:', err);
        this.message.error('Ошибка при загрузке диалогов');
      }
    });
  }
  
  createDialog(advertId: number | undefined, text: string) {
    this.http.post<CheckDialogResponse>(`${environment.appUrl}/api/chat/dialogs/create`, { advertId, text }).subscribe({
      next: (response) => {
        const dialogInfo = {
          advertInfo: response.advertInfo,
          dialogId: response.id,
          name: response.advertInfo.sellerName
        }
        this.setDialogInfo(dialogInfo);
      }
    })
  }

  createMessage(text: string, dialogId: number) {
    return this.http.post(`${environment.appUrl}/api/chat/dialogs/${dialogId}/messages`, { text })
  }

  getDialogMessages(dialogId: number) {
    this.http.get<Message[]>(`${environment.appUrl}/api/chat/dialogs/${dialogId}/messages`)
      .subscribe({
        next: (messages) => {
          this.messagesSubject.next(messages);
        }
      });
  }

  stopConnection(): void {

    if (this.hubConnection) {
      this.hubConnection.stop()
        .then(() => {
          console.log('SignalR connection stopped for real')
          this.isConnectionStartedSubject.next(false);
        })
        .catch((err) => {
          console.error('Error stopping SignalR connection', err)
        });
    }
  }

  ngOnDestroy(): void {
    this.stopConnection();
  }

}
