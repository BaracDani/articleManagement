import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {FormControl} from '@angular/forms';
import {Observable} from 'rxjs/Observable';
import {startWith} from 'rxjs/operators/startWith';
import {map} from 'rxjs/operators/map';
import {AdminService, IManageUserRole} from "../admin.service";
import {IUserRole} from "../admin.model";

@Component({
  selector: 'edit-user-dialog',
  templateUrl: './edit-user-dialog.component.html',
  styleUrls: ['./edit-user-dialog.component.css']
})
export class EditUserDialog {
  rolesControl: FormControl = new FormControl();
  filteredRoles: Observable<IUserRole[]>;
  errorMessage: string;

  constructor(
    private dialogRef: MatDialogRef<EditUserDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private adminService: AdminService) { }

  ngOnInit() {
    this.filteredRoles = this.rolesControl.valueChanges.pipe(
      startWith<string | IUserRole>(''),
      map(value => typeof value === 'string' ? value : value.name),
      map(val => this.filter(val))
    );
  }

  filter(name: string): IUserRole[] {
    return this.data.roles.filter(option => option.name.toLowerCase().indexOf(name.toLowerCase()) === 0);
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onAddRole(): void {
    let data: IManageUserRole = {
      id: this.rolesControl.value.id,
      enrolledUser: this.data.user.id
    };
    this.adminService.manageUserRole(data).subscribe((_) => {

      this.dialogRef.close();
    }, (error: any) => {
      this.errorMessage = error;
    });
  }

  onRemoveRole(): void {
    let data: IManageUserRole = {
      id: this.rolesControl.value.id,
      removedUser: this.data.user.id
    };

    this.adminService.manageUserRole(data).subscribe((_) => {

      this.dialogRef.close();
    }, (error: any) => {
      this.errorMessage = error;
    });
  }

  displayFn(userRole?: IUserRole): string | undefined {
    return userRole ? userRole.name : undefined;
  }

}
