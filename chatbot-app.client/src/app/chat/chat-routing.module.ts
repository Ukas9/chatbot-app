import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from './pages/users/users.component';
import { ChatComponent } from './pages/chat/chat.component';

const routes: Routes = [
  {
    path:'',
    component: UsersComponent
  },
  {
    path:'user/:id',
    component: ChatComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ChatRoutingModule { }
