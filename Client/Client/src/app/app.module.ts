import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms'; // <-- NgModel lives here
import {FlexLayoutModule} from '@angular/flex-layout';
import {LayoutModule} from '@angular/cdk/layout';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HttpClientModule} from '@angular/common/http';
import {AlertModule} from 'ngx-bootstrap';

import {AppComponent} from './app.component';
import {AppRoutingModule} from './app-routing.module';
import {HomePageComponent} from './homePage/homePage.component';
import {CommonService} from './core/repository/common.service';
import {UserModule} from './account/user.module';
import {AdminPageComponent} from './admin/admin-page.component';
import {AdminService} from './admin/admin.service';
import {AngularMaterialModule} from './angular-material.module';
import {EditUserDialog} from './admin/EditUser/edit-user-dialog.component';
import {ArticleService} from './article/article.service';
import {AddArticleDialog} from './article/AddArticle/add-article-dialog.component';
import {ArticleComponent} from './article/article.component';
import {ReviewComponent} from './article/review/review.component';
import {JournalComponent} from './journal/journal.component';
import {JournalService} from './journal/journal.service';
import {AddJournalDialog} from './journal/AddJournal/add-journal-dialog.component';


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    AdminPageComponent,
    EditUserDialog,
    JournalComponent,
    AddJournalDialog,
    AddArticleDialog,
    ArticleComponent,
    ReviewComponent
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
  entryComponents: [EditUserDialog, AddArticleDialog, AddJournalDialog],
  providers: [CommonService, AdminService, ArticleService, JournalService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
