import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {FlexLayoutModule} from '@angular/flex-layout';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';


import {LoginComponent} from './login/login.component';
import {AuthGuard} from './auth.guard';
import {UserService} from './user.service';
import {UserProfile} from './user.profile'
import {RegisterComponent} from '../register/register.component';

@NgModule({
  imports: [
    HttpClientModule,
    FlexLayoutModule,
    FormsModule,
    RouterModule.forChild([
      {path: 'login', component: LoginComponent},
      {path: 'register', component: RegisterComponent},
    ])
  ],
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  providers: [
    UserService,
    AuthGuard,
    UserProfile
  ]
})
export class UserModule {
}
