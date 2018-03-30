import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms'; // <-- NgModel lives here
import {FlexLayoutModule} from '@angular/flex-layout';
import {LayoutModule} from '@angular/cdk/layout';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import {AppComponent} from './app.component';
import {AppRoutingModule} from './app-routing.module';
import {AlertModule} from 'ngx-bootstrap';
import {HomePageComponent} from './homePage/homePage.component';
import {HttpClientModule} from '@angular/common/http';
import {CommonService} from './core/repository/common.service';
import {UserModule} from './account/user.module';
import {AdminPageComponent} from './admin/admin-page.component';
import {AdminService} from './admin/admin.service';
import {AngularMaterialModule} from './angular-material.module';
import {EditUserDialog} from './admin/EditUser/edit-user-dialog.component';


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    AdminPageComponent,
    EditUserDialog
  ],
  imports: [
    AlertModule.forRoot(),
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    FlexLayoutModule,
    UserModule,
    AngularMaterialModule,
    LayoutModule,
    BrowserAnimationsModule
  ],
  entryComponents: [EditUserDialog],
  providers: [CommonService, AdminService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
