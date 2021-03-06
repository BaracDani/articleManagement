import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import {HttpResponse} from '@angular/common/http';
import {MatSnackBar} from '@angular/material';

@Injectable()
export class CommonService {
  private baseUrl = 'http://localhost:52838';

  constructor(public snackBar: MatSnackBar) {
  }

  getBaseUrl(): string {
    return this.baseUrl;
  }

  handleFullError(error: HttpResponse<any>) {
    return Observable.throw(error);
  }

  handleErrorDeprecated(error: HttpResponse<any>): Observable<any> {
    let errorMessage = error.statusText;
    console.error(errorMessage);
    return Observable.throw(errorMessage || 'Server error');
  }

  handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      this.snackBar.open(`${operation} failed: ${error.message}`, 'Close', {
        duration: 10000,
      });
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      //return of(result as T);
      return Observable.throw(error);
    };
  }
}
