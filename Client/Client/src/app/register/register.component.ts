import {Component} from '@angular/core';
import {IRegisterUser} from './user';
import {UserService} from '../account/user.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})

export class RegisterComponent {

  pageTitle: string = 'Register';

  user: IRegisterUser;
  firstname: string;
  lastname: string;
  email: string;
  password: string;
  confirmPassword: string;

  registerMessage: string;
  errorMessage: string;

  constructor(private _userService: UserService) {

    this.user = {
      firstname: "",
      lastname: "",
      email: "",
      password: "",
      confirmPassword: ""
    };
  }

  registerClick() {

    this.user = {
      firstname: this.firstname,
      lastname: this.lastname,
      email: this.email,
      password: this.password,
      confirmPassword: this.confirmPassword
    };

    this._userService.register(this.user)
      .subscribe(
        response => this.onRegisterResponse(response),
        error => this.errorMessage = <any>error);
  }

  onRegisterResponse(response: any) {
    this.registerMessage = "Register with success!";

  }

}
