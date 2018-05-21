import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomePageComponent} from './homePage/homePage.component';
import {AuthGuard} from './account/auth.guard';
import {AdminPageComponent} from './admin/admin-page.component';
import {ArticleComponent} from './article/article.component';
import {ReviewComponent} from './article/review/review.component';
import {JournalComponent} from './journal/journal.component';
import {ReviewArticleComponent} from './article/reviewArticle/review-article.component';

const routes: Routes = [
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {
    path: 'home', component: HomePageComponent
  },
  {
    path: 'journal', canActivate: [AuthGuard], component: JournalComponent
  },
  {
    path: 'article', canActivate: [AuthGuard], component: ArticleComponent
  },
  {
    path: 'admin', canActivate: [AuthGuard], component: AdminPageComponent
  },
  {
    path: 'review', canActivate: [AuthGuard], component: ReviewComponent
  },
  {
    path: 'reviewArticle', canActivate: [AuthGuard], component: ReviewArticleComponent
  }
];

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]
})
export class AppRoutingModule {
}
