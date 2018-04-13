import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomePageComponent} from './homePage/homePage.component';
import {AuthGuard} from './account/auth.guard';
import {AdminPageComponent} from './admin/admin-page.component';
import {ArticleComponent} from './article/article.component';

const routes: Routes = [
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {
    path: 'home', component: HomePageComponent
  },
  {
    path: 'article', canActivate: [AuthGuard], component: ArticleComponent
  },
  {
    path: 'admin', canActivate: [AuthGuard], component: AdminPageComponent
  }
];

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]
})
export class AppRoutingModule {
}
