import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';

import { NgZorroAntdModule } from 'src/app/ng-zorro-antd.module';
import { AuthGuardAdminService } from './auth/auth-guard-admin.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtAdminInterceptor } from './auth/jwt-admin.interceptor';
import { ErrorAdminInterceptor } from './auth/error-admin.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { LayoutComponent } from './layout/layout.component';
import { UserComponent } from './user/user.component';
import { UserDetailComponent } from './user/user-detail/user-detail.component';
import { LogoutComponent } from './logout/logout.component';
const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'logout',
    component: LogoutComponent
  },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuardAdminService],
    children: [
      {
        path: 'user',
        component: UserComponent
      }
    ]
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
    NgZorroAntdModule,
    NgxSpinnerModule,
  ],
  declarations: [
    DashboardComponent,
    LoginComponent,
    LogoutComponent,
    PageNotFoundComponent,
    LayoutComponent,
    UserComponent,
    UserDetailComponent
  ],
  providers: [
    AuthGuardAdminService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtAdminInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorAdminInterceptor, multi: true },
  ]
})
export class AdminModule { }
