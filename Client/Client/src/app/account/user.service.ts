import {IProfile} from './user.model';
import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {catchError, tap} from 'rxjs/operators';
import {Router} from '@angular/router';
import {CommonService} from '../core/repository/common.service';
import {UserProfileService} from './user.profile';
import {IRegisterUser} from '../register/user';import 'rxjs/add/operator/finally';

@Injectable()
export class UserService {
  redirectUrl: string;

  constructor(private _http: HttpClient,
              private router: Router,
              private authProfile: UserProfileService,
              private commonService: CommonService) {

  }

  isAuthenticated() {
    let profile: IProfile = this.authProfile.getProfile();
    let validToken = profile.token != "" && profile.token != null;
    let isTokenExpired = this.isTokenExpired(profile);
    return validToken && !isTokenExpired;
  }

  isAuthorized() {
    let profile = this.authProfile.getProfile();
    let validToken = profile.token != "" && profile.token != null;
    let isTokenExpired = this.isTokenExpired(profile);
    return validToken && !isTokenExpired;
  }

  isUserAdmin() {
    let profile = this.authProfile.getProfile();
    let validToken = profile.token != "" && profile.token != null;
    let isTokenExpired = this.isTokenExpired(profile);
    let isAdmin: boolean = false;
    if (profile && profile.roles && profile.roles.length > 0) {
      profile.roles.forEach((role: string) => {
          if (role === 'Admin') {
            isAdmin = true;
          }
        }
      );
    }
    return validToken && !isTokenExpired && isAdmin;
  }

  isTokenExpired(profile: IProfile) {
    let expiration = new Date(profile.expiration)
    return expiration < new Date();
  }

  login(email, password): Observable<any> {
    let creds = "grant_type=password" + "&username=" + email + "&password=" + password;
    const headers = new HttpHeaders({'Content-Type': 'application/x-www-form-urlencoded'});
    let url = this.commonService.getBaseUrl() + '/Token';

    return this._http.post(url, creds, {headers: headers})
      .map((response: IProfile) => {
        //var userProfile: IProfile = response.json();
        let userProfile: IProfile = response;
        userProfile.roles = eval('(' + response.roles + ')');
        this.authProfile.setProfile(userProfile);
        return response;
      }).pipe(tap(_ => console.log(`Log Login`)),
        catchError(this.commonService.handleError<any>('login')));
  }

  register(user: IRegisterUser): Observable<any> {

    let creds = "firstname=" + user.firstname + "&lastname=" + user.lastname + "&email=" + user.email
      + "&password=" + user.password + "&confirmPassword=" + user.confirmPassword;
    const headers = new HttpHeaders({'Content-Type': 'application/x-www-form-urlencoded'});
    let url = this.commonService.getBaseUrl() + '/api/account/register';

    return this._http.post(url, creds, {headers: headers}).pipe(
      tap(_ => console.log(`User register`)),
      catchError(this.commonService.handleError<any>('register')));
  }

  userInfo() {
    let headers = new HttpHeaders({'Content-Type': 'application/x-www-form-urlencoded'});
    if (this.isAuthenticated()) {
      headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    }
    let url = this.commonService.getBaseUrl() + '/api/account/userinfo';
    return this._http.get(url, {headers: headers}).pipe(
      tap(_ => console.log(`Log User info`)),
      catchError(this.commonService.handleError<any>('userInfo')));
  }

  logout() {
    let headers = new HttpHeaders({'Content-Type': 'application/x-www-form-urlencoded'});
    if (this.isAuthenticated()) {
      headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    }

    let url = this.commonService.getBaseUrl() + '/api/account/logout';

    return this._http.post(url, null, {headers: headers}).pipe(
      tap(_ => console.log(`Logout`)),
      catchError(this.commonService.handleError<any>('logout')));

  }
}
