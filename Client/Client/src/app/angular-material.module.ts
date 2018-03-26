import {NgModule} from '@angular/core';
import {MatListModule} from '@angular/material/list';
import {MatTableModule} from '@angular/material/table';

@NgModule({
  imports: [MatListModule, MatTableModule],
  exports: [MatListModule, MatTableModule],
})
export class AngularMaterialModule { }
