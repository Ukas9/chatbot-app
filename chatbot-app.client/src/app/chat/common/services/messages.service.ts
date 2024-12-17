import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MessageDto } from '../models/message.dto';

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
}
