import {Component} from '@angular/core';
import {Router} from '@angular/router';

import {UserService} from '../user.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})

export class LoginComponent {

  pageTitle: string = 'Login';

  //model
  email: string;
  password: string;

  loginMessage: string;
  errorMessage: string;

  constructor(private userService: UserService,
              private router: Router) {
  }

  loginClick() {

    if (!this.email || !this.password) {
      return;
    }
    this.userService.login(this.email, this.password)
      .subscribe((result: any) => {
        let res = result;
        console.log(res);
        this.onLoginResponse(result);

        if (this.userService.redirectUrl) {
          this.router.navigateByUrl(this.userService.redirectUrl);
        } else {
          this.router.navigate(['/home']);
        }
      }, (error: any) => {
        this.errorMessage = error;
      });
  }

  onLoginResponse(response: any) {
    this.loginMessage = (response.access_token ? "Login with success!" : "Invalid data!");
    this.userService.emitLoginChange();
  }
}
