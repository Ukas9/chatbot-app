import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MessageDto } from '../models/message.dto';
import { SendMessageCommand } from '../models/send-message.command';

@Injectable({
  providedIn: 'root'
})
export class MessagesService {
  private readonly baseApiUrl = `${environment.apiUrl}/api/messages`;

  constructor(private readonly httpClient: HttpClient) {
  }

  public getAllMessages(conversationId: number): Observable<MessageDto[]> {
    return this.httpClient.get<MessageDto[]>(`${this.baseApiUrl}/conversation/${conversationId}`);
  }

  public sendMessage(data: SendMessageCommand): Observable<MessageDto>{
    return this.httpClient.post<MessageDto>(this.baseApiUrl, data);
  }
}
