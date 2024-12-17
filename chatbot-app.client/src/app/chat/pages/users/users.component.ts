import { Component } from '@angular/core';
import { UsersService } from '../../common/services/users.service';
import { BehaviorSubject, finalize, Observable, switchMap, take } from 'rxjs';
import { UserDto } from '../../common/models/user.dto';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrl: './users.component.css',
})
export class UsersComponent {
  public users$: Observable<UserDto[]>;

  private refresh$: BehaviorSubject<void> = new BehaviorSubject<void>(undefined);

  constructor(private readonly usersService: UsersService) {
    this.users$ =
      this.refresh$.pipe(
        switchMap(() => this.usersService.getUsers(),
        ));
  }

  public onUserClick(event: UserDto) {
    console.log(event);
  }

  public onUserAdd(username: string) {
    this.usersService.createUser(username)
      .pipe(
        take(1),
        finalize(() => this.refresh$.next(undefined)),
      )
      .subscribe(console.log);
    console.log(username);
  }
}
