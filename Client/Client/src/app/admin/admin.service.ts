import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Router} from '@angular/router';
import {CommonService} from '../core/repository/common.service';
import {Observable} from 'rxjs/Observable';
import {catchError, tap} from 'rxjs/operators';
import {UserProfileService} from '../account/user.profile';
import {IUserModel, IUserRole} from './admin.model';

export interface IManageUserRole {
  id: string;
  enrolledUser?: string;
  removedUser?: string;
}


@Injectable()
export class AdminService {

  constructor(private _http: HttpClient,
              private router: Router,
              private commonService: CommonService,
              private authProfile: UserProfileService) {

  }

  getUsers(): Observable<IUserModel[]> {

    let headers = new HttpHeaders({'Content-Type': 'application/x-www-form-urlencoded'});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/account/users';

    return this._http.get(url, {headers: headers}).pipe(
      tap(_ => console.log(`Get users`)),
      catchError(this.commonService.handleError<any>('getUsers')));
  }

  getRoles(): Observable<IUserRole[]> {

    let headers = new HttpHeaders();
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/roles';

    return this._http.get(url, {headers: headers}).pipe(
      tap(_ => console.log(`Get roles`)),
      catchError(this.commonService.handleError<any>('getRoles')));
  }

  manageUserRole(data: IManageUserRole): Observable<any> {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/roles/ManageUserInRole';

    return this._http.post(url,data, {headers: headers}).pipe(
      tap(_ => console.log(`manageUserRole`)),
      catchError(this.commonService.handleError<any>('manageUserRole')));
  }

}
