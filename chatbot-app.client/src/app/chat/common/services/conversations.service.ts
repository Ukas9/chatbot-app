import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ConversationsService {
  private readonly baseApiUrl = `${environment.apiUrl}/api/conversations`;

  constructor(private readonly httpClient: HttpClient) {
  }

  public getLastConversation(userId: number): Observable<number> {
    return this.httpClient.get<number>(`${this.baseApiUrl}/${userId}`);
  }

  public createConversation(userId: number): Observable<number> {
    return this.httpClient.post<number>(this.baseApiUrl, {userId: userId}, {});
  }
}
