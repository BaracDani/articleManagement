import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {FormControl} from '@angular/forms';
import {Observable} from 'rxjs/Observable';
import {startWith} from 'rxjs/operators/startWith';
import {map} from 'rxjs/operators/map';

@Component({
  selector: 'edit-user-dialog',
  templateUrl: './edit-user-dialog.component.html',
})
export class EditUserDialog {
  rolesControl: FormControl = new FormControl();
  filteredRoles: Observable<string[]>;

  constructor(
    public dialogRef: MatDialogRef<EditUserDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.filteredRoles = this.rolesControl.valueChanges.pipe(
      startWith(''),
      map(val => this.filter(val))
    );
  }

  filter(val: string): string[] {
    return this.data.roles.filter(option => option.name.toLowerCase().indexOf(val.toLowerCase()) === 0);
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
