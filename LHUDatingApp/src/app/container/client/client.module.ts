import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { AuthGuardService } from './auth/auth-guard.service';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { ChatDatePipe } from 'src/app/core/pipe/chat-date.pipe';
import { PipeHostFilePipe } from 'src/app/core/pipe/pipe-host-file.pipe';
import { MessageDetailComponent } from './message/template/message/message-detail/message-detail.component';
import { MessageComponent } from './message/message.component';
import { ListMessageComponent } from './message/template/message/list-message/list-message.component';
import { DefaultComponent } from './message/template/default/default.component';
import { ButtonUploadComponent } from './message/template/button-upload/button-upload.component';
import { ContactDetailComponent } from './message/template/contact/contact-detail/contact-detail.component';
import { ListContactComponent } from './message/template/contact/list-contact/list-contact.component';
import { ListNotificationComponent } from './message/template/notification/list-notification/list-notification.component';
import { NotificationDetailComponent } from './message/template/notification/notification-detail/notification-detail.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './auth/jwt.interceptor';
import { ErrorInterceptor } from './auth/error.interceptor';
import { AppComponent } from 'src/app/app.component';
import { ProfileComponent } from './profile/profile.component';
import { MatGridListModule } from '@angular/material/grid-list';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import { MemberCardComponent } from './home/member/member-card/member-card.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'message',
    component: MessageComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: '',
    component: HomeComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: "**",
    component: PageNotFoundComponent
  }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    MatGridListModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule
  ],
  declarations: [
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    PageNotFoundComponent,
    ProfileComponent,
    MessageComponent,
    ListContactComponent,
    ContactDetailComponent,
    ListMessageComponent,
    MessageDetailComponent,
    ListNotificationComponent,
    NotificationDetailComponent,
    DefaultComponent,
    ButtonUploadComponent,
    MemberCardComponent,
    ChatDatePipe,
    PipeHostFilePipe
  ],
  providers: [
    AuthGuardService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ]
})
export class ClientModule { }
