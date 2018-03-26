import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomePageComponent} from './homePage/homePage.component';
import {AuthGuard} from './account/auth.guard';
import {AdminPageComponent} from './admin/admin-page.component';

const routes: Routes = [
  {path: '', redirectTo: '/login', pathMatch: 'full'},
  {
    path: 'home', canActivate: [AuthGuard], component: HomePageComponent
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
