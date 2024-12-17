import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { of, switchMap, take, tap } from 'rxjs';
import { ConversationsService } from '../../common/services/conversations.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent {

  private userId: number | undefined;
  private conversationId: number | undefined;

  constructor(private readonly activatedRoute: ActivatedRoute, private readonly conversationsService: ConversationsService) {
    activatedRoute.params
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
      )
      .subscribe();
  }

}

