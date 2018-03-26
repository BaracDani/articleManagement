import {Component, OnInit} from '@angular/core';
import {AdminService} from "./admin.service";
import {IUserModel} from "./admin.model";
import {MatIconRegistry} from "@angular/material";

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

  constructor(private adminService: AdminService,
              private MatIconRegistry: MatIconRegistry) {
  }

  ngOnInit(): void {

    this.adminService.getUsers()
      .subscribe((result: IUserModel[]) => {
        this.userList = result;
        console.log(this.userList);
      }, (error: any) => {
        this.errorMessage = error;
      });
  }
}
