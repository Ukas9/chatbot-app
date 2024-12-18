import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, combineLatest, finalize, interval, map, Observable, of, switchMap, take, tap } from 'rxjs';
import { ConversationsService } from '../../common/services/conversations.service';
import { MessagesService } from '../../common/services/messages.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageDto } from '../../common/models/message.dto';
import { MessageTypeEnum } from '../../common/models/message-type.enum';
import { SendMessageCommand } from '../../common/models/send-message.command';
import { RateMessageCommand } from '../../common/models/rate-message.command';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent {
  private messagesSubject = new BehaviorSubject<MessageDto[]>([]);
  public chatForm: FormGroup;
  public messages$: Observable<MessageDto[]> = this.messagesSubject.asObservable();

  private userId: number | undefined;
  private conversationId: number | undefined;

  private refresh$: BehaviorSubject<void> = new BehaviorSubject<void>(undefined);


  constructor(activatedRoute: ActivatedRoute,
              private readonly conversationsService: ConversationsService,
              private readonly messagesService: MessagesService) {
    this.chatForm = new FormGroup({
      message: new FormControl('', [Validators.required]),
    });

    combineLatest([
      activatedRoute.params, this.refresh$,
    ])
      .pipe(
        take(1),
        switchMap(([data]) => {
          if (data['id']) {
            this.userId = +data['id'];
            return this.conversationsService.getLastConversation(this.userId);
          }
          return of(null);
        }),
        switchMap(data => {
          if (data === null) {
            return of(0);
          }

          if (data === 0) {
            return this.conversationsService.createConversation(this.userId!);
          }

          return of(data);
        }),
        tap(conversationId =>
          this.conversationId = conversationId,
        ),
        switchMap((conversationId) => {

          return this.messagesService.getAllMessages(conversationId);
        }),
        map(messages => {
          this.updateMessages(messages);
        }),
      ).subscribe();
  }

  public sendMessage() {

    if (!this.chatForm.valid) {
      return;
    }
    const sendMessageCommand: SendMessageCommand = {
      message: this.chatForm.value.message,
      conversationId: this.conversationId!,
      userId: this.userId!,
    };
    const msgDto: MessageDto = {
      content: sendMessageCommand.message,
      type: MessageTypeEnum.User,
      createdAt: new Date(),
      id: 0,
    };
    this.addMessage(msgDto);

    this.messagesService.sendMessage(sendMessageCommand)
      .pipe(take(1),
        tap(response => {
          this.addMessage(response);
        }),
        finalize(() => {
          this.chatForm.reset();
        }),
      )
      .subscribe({
        error: (err) => console.error('Failed to send message:', err),
      });
  }

  public getMessageClass(type: MessageTypeEnum): string {
    return MessageTypeEnum[type];
  }

  public cancelClick() {
    console.log('cancel click');
  }

  public onLikeClick(messageId: number) {
    const rateMsg: RateMessageCommand = {
      messageId: messageId,
      likeDislike: 1,
    };

    this.rateMessage(rateMsg);

  }

  public onDislikeClick(messageId: number) {
    const rateMsg: RateMessageCommand = {
      messageId: messageId,
      likeDislike: -1,
    };
    this.rateMessage(rateMsg);
  }

  private rateMessage(data: RateMessageCommand) {
    this.messagesService.rateMessage(data)
      .pipe(take(1),
        finalize(() => {
          const message = this.messagesSubject.value.find(x => x.id === data.messageId);
          message!.likeDislike = data.likeDislike;
          this.updateMessage(message!);
        }),
      )
      .subscribe({
        error: (err) => console.error('Failed to rate message:', err),
      });
  }

  private updateMessages(newMessages: MessageDto[]): void {
    const currentMessages = this.messagesSubject.value;
    this.messagesSubject.next([...currentMessages, ...newMessages]);
  }

  private addMessage(newMessage: MessageDto): void {
    const currentMessages = this.messagesSubject.value;
    this.messagesSubject.next([...currentMessages, newMessage]);
  }

  private   updateMessage(updatedMessage: MessageDto): void {
    const currentMessages = this.messagesSubject.value;

    const updatedMessages = currentMessages.map((msg) =>
      msg.id === updatedMessage.id ? { ...msg, ...updatedMessage } : msg
    );

    this.messagesSubject.next(updatedMessages);
  }
}

