import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {catchError, tap} from 'rxjs/operators';
import {Router} from '@angular/router';
import {CommonService} from '../core/repository/common.service';
import 'rxjs/add/operator/finally';
import {UserProfileService} from "../account/user.profile";
import {ResponseContentType} from "@angular/http";
import {IUserModel} from "../admin/admin.model";


export interface IArticle {
  id: number;
  userId: string;
  title: string;
  author: string;
  abstract: string;
  approvalStatus: number;
  deadline: string;
  journalId: number;
  filePath: string;
}

export interface IAddArticle {
  title: string;
  abstract: string;
  author: string;
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

  getPendingArticles(id: number): Observable<IArticle[]> {

    let headers = new HttpHeaders();
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/article/pendings';
    let params = new HttpParams().set('journalId', id.toString());

    return this._http.get(url, {headers: headers, params: params}).pipe(
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

  getUserToReviewArticles(): Observable<IArticle[]> {

    let headers = new HttpHeaders();
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/article/inReview';

    return this._http.get(url, {headers: headers}).pipe(
      tap(_ => console.log(`Get in review articles`)),
      catchError(this.commonService.handleError<any>('getInReviewArticles')));
  }

  getFile(filePath: string): Observable<any> {
    let headers = new HttpHeaders({'Content-Type': 'application/x-www-form-urlencoded'});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/article/downloadFile';
    let params = new HttpParams().set('fileName', filePath);

    return this._http.get(url, {headers: headers, params: params, responseType: 'blob'}).pipe(
      tap(_ => console.log(`Download file`)),
      catchError(this.commonService.handleError<any>('downloadFile')));
  }

  createArticle(data: FormData): Observable<any> {
    let headers = new HttpHeaders({});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/article/postArticle';

    return this._http.post(url, data, {headers: headers}).pipe(
      tap(_ => console.log(`addArticle`)),
      catchError(this.commonService.handleError<any>('addArticle')));
  }

  approveArticle(article: IArticle, userIds: string[]): Observable<any> {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/article/editorApprove';

    return this._http.put(url, {article: article, userIds}, {headers: headers}).pipe(
      tap(_ => console.log(`Editor approveArticle`)),
      catchError(this.commonService.handleError<any>('Editor approveArticle')));
  }

  rejectArticle(article: IArticle): Observable<any> {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/article/editorReject';

    return this._http.put(url, article, {headers: headers}).pipe(
      tap(_ => console.log(`Editor rejectArticle`)),
      catchError(this.commonService.handleError<any>('Editor rejectArticle')));
  }

  reviewArticle(article: IArticle, reviewPoints: number, comment: string): Observable<any> {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/article/userReview';

    return this._http.put(url, {article: article, reviewPoints: reviewPoints, comment: comment}, {headers: headers}).pipe(
      tap(_ => console.log(`UserReview approveArticle`)),
      catchError(this.commonService.handleError<any>('UserReview approveArticle')));
  }

  abstainReviewArticle(article: IArticle): Observable<any> {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/article/userReviewAbstain';

    return this._http.put(url, article, {headers: headers}).pipe(
      tap(_ => console.log(`UserReview Abstain approveArticle`)),
      catchError(this.commonService.handleError<any>('UserReview Abstain approveArticle')));
  }

  getUsersOfDomain(domainId: number, userId: string): Observable<IUserModel[]> {

    let headers = new HttpHeaders({'Content-Type': 'application/x-www-form-urlencoded'});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/domain/users';
    let params = new HttpParams().set('domainId', domainId.toString());
    params = params.append('userId', userId);

    return this._http.get(url, {headers: headers, params: params}).pipe(
      tap(_ => console.log(`Get users`)),
      catchError(this.commonService.handleError<any>('getUsers')));
  }

  getReviewsForArticle(article: IArticle): Observable<any[]> {

    let headers = new HttpHeaders();
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/article/reviewsForArticle';
    let params = new HttpParams().set('articleId', article.id.toString());

    return this._http.get(url, {headers: headers, params: params}).pipe(
      tap(_ => console.log(`Get reviews for articles`)),
      catchError(this.commonService.handleError<any>('getReviewsForArticles')));
  }

}
