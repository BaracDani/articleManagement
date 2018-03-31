import {Component, OnInit} from '@angular/core';
import {AdminService} from "./admin.service";
import {IUserModel, IUserRole} from "./admin.model";
import {MatDialog} from "@angular/material";
import {EditUserDialog} from "./EditUser/edit-user-dialog.component";

@Component({
  selector: 'adminPage',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.css']
})

export class AdminPageComponent implements OnInit {

  pageTitle: string = 'AdminPage';
  errorMessage: string;
  userList: IUserModel[] = [];
  displayedColumns = ['fullName', 'email','action'];
  rolesList: IUserRole[] = [];

  constructor(private adminService: AdminService,
              private dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.adminService.getUsers()
      .subscribe((result: IUserModel[]) => {
        this.userList = result;
        console.log(this.userList);
      }, (error: any) => {
        this.errorMessage = error;
      });
    this.adminService.getRoles()
      .subscribe((result: IUserRole[]) => {
        this.rolesList = result;
        console.log(this.rolesList);
      }, (error: any) => {
        this.errorMessage = error;
      });
  }

  editUser(user) {
    let dialogRef = this.dialog.open(EditUserDialog, {
      width: '450px',
      data: {user: user, roles: this.rolesList}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.adminService.getUsers()
        .subscribe((result: IUserModel[]) => {
          this.userList = result;
        }, (error: any) => {
          this.errorMessage = error;
        });
    });
  }
}
