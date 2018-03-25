import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms'; // <-- NgModel lives here
import {FlexLayoutModule} from '@angular/flex-layout';


import {AppComponent} from './app.component';
import {AppRoutingModule} from './/app-routing.module';
import {AlertModule} from 'ngx-bootstrap';
import {HomePageComponent} from './homePage/homePage.component';
import {HttpClientModule} from '@angular/common/http';
import {CommonService} from './core/repository/common.service';
import {UserModule} from './account/user.module';


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
  ],
  imports: [
    AlertModule.forRoot(),
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    FlexLayoutModule,
    UserModule
  ],
  providers: [CommonService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
