import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import { FormsModule } from '@angular/forms'; // <-- NgModel lives here


import {AppComponent} from './app.component';
import {AppRoutingModule} from './/app-routing.module';
import {AlertModule} from 'ngx-bootstrap';
import {HomePageComponent} from './homePage/homePage.component';
import {RegisterService} from './register/register.service';
import {RegisterComponent} from './register/register.component';
import {HttpClientModule} from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    RegisterComponent
  ],
  imports: [
    AlertModule.forRoot(),
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [RegisterService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
