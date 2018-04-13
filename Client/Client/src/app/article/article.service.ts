import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {catchError, tap} from 'rxjs/operators';
import {Router} from '@angular/router';
import {CommonService} from '../core/repository/common.service';
import 'rxjs/add/operator/finally';
import {UserProfileService} from "../account/user.profile";


export interface IArticle {
  title: string;
  author: string;
  abstract: string;
  id?: number;
  approvalStatus?: number;
  userId?: string;
}

@Injectable()
export class ArticleService {

  constructor(private _http: HttpClient,
              private router: Router,
              private commonService: CommonService,
              private authProfile: UserProfileService) {

  }


  getArticles(): Observable<IArticle[]> {

    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    let url = this.commonService.getBaseUrl() + '/api/article';

    return this._http.get(url, {headers: headers}).pipe(
      tap(_ => console.log(`Get articles`)),
      catchError(this.commonService.handleError<any>('getArticles')));
  }

  getPendingArticles(): Observable<IArticle[]> {

    let headers = new HttpHeaders();
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/article/pendings';

    return this._http.get(url, {headers: headers}).pipe(
      tap(_ => console.log(`Get pending articles`)),
      catchError(this.commonService.handleError<any>('getPendingArticles')));
  }

  getUserArticles(): Observable<IArticle[]> {

    let headers = new HttpHeaders();
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/article/userArticle';

    return this._http.get(url, {headers: headers}).pipe(
      tap(_ => console.log(`Get user articles`)),
      catchError(this.commonService.handleError<any>('getUserArticles')));
  }

  createArticle(data: IArticle): Observable<any> {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/article';

    return this._http.post(url, data, {headers: headers}).pipe(
      tap(_ => console.log(`addArticle`)),
      catchError(this.commonService.handleError<any>('addArticle')));
  }

}
