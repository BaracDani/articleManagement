import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {FlexLayoutModule} from '@angular/flex-layout';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';


import {LoginComponent} from './login/login.component';
import {AuthGuard} from './auth.guard';
import {UserService} from './user.service';
import {UserProfileService} from './user.profile'
import {RegisterComponent} from '../register/register.component';
import {AngularMaterialModule} from '../angular-material.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {LayoutModule} from "@angular/cdk/layout";
import {BrowserModule} from "@angular/platform-browser";

@NgModule({
  imports: [
    HttpClientModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialModule,
    BrowserAnimationsModule,
    BrowserModule,
    LayoutModule,
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
    UserProfileService
  ]
})
export class UserModule {
}
