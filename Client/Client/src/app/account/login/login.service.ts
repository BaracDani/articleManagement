import {ILoginAccount} from './IAccount';
import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs/Observable";
import {catchError, tap} from "rxjs/operators";
import {of} from "rxjs/observable/of";

@Injectable()
export class LoginService {
  private _loginUrl = 'http://localhost:52838/Token';
  private _userInfoUrl = 'http://localhost:52838/api/account/userinfo';

  constructor(private _http: HttpClient) {

  }

  login(user: ILoginAccount): Observable<any> {

    let creds = "grant_type=password"+"&username=" + user.email
      + "&password=" + user.password;
    const headers = new HttpHeaders({'Content-Type': 'application/x-www-form-urlencoded'});
    //const headers = new HttpHeaders({'Access-Control-Allow-Origin': '*'});
	
    //headers.append('Content-Type', 'application/x-www-form-urlencoded');
    //headers.append('Authorization', 'Basic 1234');

    return this._http.post(this._loginUrl, creds, {headers: headers}).pipe(
        tap(_ => console.log(`Log Login`)),
        catchError(this.handleError<any>('login')));
  }
  
  userInfo() {
	  let headers = new HttpHeaders({'Content-Type': 'application/x-www-form-urlencoded'});
	  let token = localStorage.getItem('token');
if (token) {
    headers = headers.append('Authorization','Bearer ' + token);
}
	  return this._http.get(this._userInfoUrl, {headers: headers}).pipe(
        tap(_ => console.log(`Log User info`)),
        catchError(this.handleError<any>('userInfo')));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
