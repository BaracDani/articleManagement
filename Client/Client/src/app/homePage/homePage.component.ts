import { Component } from '@angular/core';
import {LoginService} from '../account/login/login.service';

@Component({
    selector: 'homePage',
    templateUrl: './homePage.component.html',
    styleUrls: ['./homePage.component.css']
})

export class HomePageComponent {

    pageTitle: string = 'HomePage';
  errorMessage: string;

    constructor(private _loginService: LoginService) {
    }

	userInfo() {
		this._loginService.userInfo()
      .subscribe((result: any) => {
    let res = result;
    console.log(res);
},(error: any) => {
    this.errorMessage = error;
});
	}
}
