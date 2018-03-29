import {NgModule} from '@angular/core';
import {MatListModule} from '@angular/material/list';
import {MatTableModule} from '@angular/material/table';
import {MatIconModule} from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatButtonModule} from '@angular/material';

@NgModule({
  imports: [
    MatListModule,
    MatTableModule,
    MatIconModule,
    MatToolbarModule,
    MatSidenavModule,
    MatButtonModule],
  exports: [MatListModule,
    MatTableModule,
    MatIconModule,
    MatToolbarModule,
    MatSidenavModule,
    MatButtonModule]
})
export class AngularMaterialModule {
}
