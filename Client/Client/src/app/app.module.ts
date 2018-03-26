import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms'; // <-- NgModel lives here
import {FlexLayoutModule} from '@angular/flex-layout';


import {AppComponent} from './app.component';
import {AppRoutingModule} from './app-routing.module';
import {AlertModule} from 'ngx-bootstrap';
import {HomePageComponent} from './homePage/homePage.component';
import {HttpClientModule} from '@angular/common/http';
import {CommonService} from './core/repository/common.service';
import {UserModule} from './account/user.module';
import {AdminPageComponent} from './admin/admin-page.component';
import {AdminService} from './admin/admin.service';


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    AdminPageComponent
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
  providers: [CommonService, AdminService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
