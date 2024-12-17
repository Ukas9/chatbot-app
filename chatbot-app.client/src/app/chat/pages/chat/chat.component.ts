import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, of, switchMap, take, tap } from 'rxjs';
import { ConversationsService } from '../../common/services/conversations.service';
import { MessagesService } from '../../common/services/messages.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageDto } from '../../common/models/message.dto';
import { MessageTypeEnum } from '../../common/models/message-type.enum';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent {
  public messages$: Observable<MessageDto[]>;
  private userId: number | undefined;
  private conversationId: number | undefined;

  constructor(private readonly activatedRoute: ActivatedRoute,
              private readonly conversationsService: ConversationsService,
              private readonly messagesService: MessagesService) {


    this.messages$ = activatedRoute.params
      .pipe(take(1),
        switchMap((data) => {
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
      );
  }

  public getMessageClass(type: MessageTypeEnum): string {
    return MessageTypeEnum[type];
  }
}

