import {IUser} from './user';
import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs/Observable";
import {catchError, tap} from "rxjs/operators";
import {of} from "rxjs/observable/of";

@Injectable()
export class RegisterService {
  private _registerUrl = 'http://localhost:52838/api/account/register';

  constructor(private _http: HttpClient) {

  }

  register(user: IUser): Observable<any> {

    var creds = "firstname=" + user.firstname + "&lastname=" + user.lastname + "&email=" + user.email
      + "&password=" + user.password + "&confirmPassword=" + user.confirmPassword;
    const headers = new HttpHeaders({'Content-Type': 'application/x-www-form-urlencoded'});
    //headers.append('Content-Type', 'application/x-www-form-urlencoded');
    //headers.append('Authorization', 'Basic 1234');

    return this._http.post(this._registerUrl, creds, {headers: headers})
      .map((response: Response) => <any>response.json()).pipe(
        tap(_ => console.log(`regggg`)),
        catchError(this.handleError<any>('register')));
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
