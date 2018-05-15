import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {catchError, tap} from 'rxjs/operators';
import {Router} from '@angular/router';
import {CommonService} from '../core/repository/common.service';
import 'rxjs/add/operator/finally';
import {UserProfileService} from "../account/user.profile";
import {IArticle} from "../article/article.service";


export interface IJournal {
  id: number;
  title: string;
  publishDate: string;
  editorId: string;
  domainId: number;
  isPublished: boolean;
}

export interface IDomain {
  id: number;
  name: string;
}

export interface IAddJournal {
  title: string;
  domainId: number;
}
@Injectable()
export class JournalService {

  constructor(private _http: HttpClient,
              private router: Router,
              private commonService: CommonService,
              private authProfile: UserProfileService) {

  }


  getJournals(): Observable<IJournal[]> {

    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    let url = this.commonService.getBaseUrl() + '/api/journal';

    return this._http.get(url, {headers: headers}).pipe(
      tap(_ => console.log(`Get journals`)),
      catchError(this.commonService.handleError<any>('getJournals')));
  }

  getJournalArticles(id: number): Observable<IArticle[]> {

    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    let url = this.commonService.getBaseUrl() + '/api/article/published';
    let params = new HttpParams().set('journalId', id.toString());

    return this._http.get(url, {headers: headers, params: params}).pipe(
      tap(_ => console.log(`Get journal articles`)),
      catchError(this.commonService.handleError<any>('getJournalArticles')));
  }

  getUserJournals(): Observable<IJournal[]> {

    let headers = new HttpHeaders();
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/journal/userJournals';

    return this._http.get(url, {headers: headers}).pipe(
      tap(_ => console.log(`Get user domains`)),
      catchError(this.commonService.handleError<any>('getUserDomains')));
  }

  createJournal(data: IAddJournal): Observable<any> {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/journal';

    return this._http.post(url, data, {headers: headers}).pipe(
      tap(_ => console.log(`add Journal`)),
      catchError(this.commonService.handleError<any>('addJournal')));
  }

  deleteJournal(data: IJournal): Observable<any> {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/journal'+'/'+data.id.toString();
    let params = new HttpParams().set('journalId', data.id.toString());

    return this._http.delete(url, {headers: headers}).pipe(
      tap(_ => console.log(`delete Journal`)),
      catchError(this.commonService.handleError<any>('deleteJournal')));
  }

  updateJournal(journal: IJournal): Observable<any> {
    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/journal';

    return this._http.put(url, journal, {headers: headers}).pipe(
      tap(_ => console.log(`update Journal`)),
      catchError(this.commonService.handleError<any>('updateJournal')));
  }

  getDomains(): Observable<IDomain[]> {

    let headers = new HttpHeaders({'Content-Type': 'application/json'});
    headers = headers.append('Authorization', 'Bearer ' + this.authProfile.getProfile().token);
    let url = this.commonService.getBaseUrl() + '/api/domain';

    return this._http.get(url, {headers: headers}).pipe(
      tap(_ => console.log(`Get domains`)),
      catchError(this.commonService.handleError<any>('getDomains')));
  }

}
