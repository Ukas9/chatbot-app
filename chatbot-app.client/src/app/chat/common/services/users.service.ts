import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { UserDto } from '../models/user.dto';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  private readonly baseApiUrl = `${environment.apiUrl}/api/users`;

  constructor(private readonly httpClient: HttpClient) {}

  public getUsers(): Observable<UserDto[]> {
    return this.httpClient.get<UserDto[]>(`${this.baseApiUrl}`);
  }

  public createUser(username:string):Observable<number>{
    return this.httpClient.post<number>(`${this.baseApiUrl}`, username, {})
  }
}
