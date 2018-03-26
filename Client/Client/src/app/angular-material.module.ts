import {NgModule} from '@angular/core';
import {MatListModule} from '@angular/material/list';
import {MatTableModule} from '@angular/material/table';
import {MatIconModule} from '@angular/material/icon';

@NgModule({
  imports: [MatListModule, MatTableModule, MatIconModule],
  exports: [MatListModule, MatTableModule, MatIconModule]
})
export class AngularMaterialModule {
}
