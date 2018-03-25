import {Component} from '@angular/core';
import {IUser} from './user';
import {RegisterService} from './register.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})

export class RegisterComponent {

  pageTitle: string = 'Register';

  user: IUser;
  firstname: string;
  lastname: string;
  email: string;
  password: string;
  confirmPassword: string;

  registerMessage: string;
  errorMessage: string;

  constructor(private _registerService: RegisterService) {

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

    this._registerService.register(this.user)
      .subscribe(
        response => this.onRegisterResponse(response),
        error => this.errorMessage = <any>error);
  }

  onRegisterResponse(response: any) {
    this.registerMessage = (response.isValid ? "Register with success!" : "Invalid data!")
    if (response.isValid) {
      localStorage.setItem('profile', JSON.stringify(response.profile));
      localStorage.setItem('token', response.id_token);
    }
    else {
      this.logout();
    }
  }

  logout() {
    localStorage.removeItem('profile');
    localStorage.removeItem('token');
  }

}
