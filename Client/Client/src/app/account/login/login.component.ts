import {Component} from '@angular/core';
import {ILoginAccount} from './IAccount';
import {LoginService} from './login.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})

export class LoginComponent {

  pageTitle: string = 'Login';

  user: ILoginAccount;
  email: string;
  password: string;

  loginMessage: string;
  errorMessage: string;

  constructor(private _loginService: LoginService) {

    this.user = {
      email: "",
      password: ""
    };
  }

  loginClick() {

    this.user = {
      email: this.email,
      password: this.password
    };

    this._loginService.login(this.user)
      .subscribe((result: any) => {
        let res = result;
        console.log(res);
        this.onLoginResponse(result);
      }, (error: any) => {
        this.errorMessage = error;
      });
  }

  onLoginResponse(response: any) {
    this.loginMessage = (response.access_token ? "Login with success!" : "Invalid data!")
    if (response.access_token) {
      localStorage.setItem('profile', JSON.stringify(response.userName));
      localStorage.setItem('token', response.access_token);
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
