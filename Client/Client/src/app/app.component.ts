import {Component, OnInit, ChangeDetectorRef} from '@angular/core';
import {IProfile} from './account/user.model';
import {UserService} from './account/user.service';
import {UserProfileService} from './account/user.profile';
import {Router} from '@angular/router';
import {MediaMatcher} from '@angular/cdk/layout';
import 'rxjs/add/operator/finally';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  pageTitle: string = 'Article Management app';
  loading: boolean = true;
  Profile: IProfile;
  mobileQuery: MediaQueryList;

  private _mobileQueryListener: () => void;

  constructor(public authService: UserService,
              private authProfile: UserProfileService,
              private router: Router,
              changeDetectorRef: ChangeDetectorRef,
              media: MediaMatcher) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }

  ngOnInit(): void {
    this.Profile = this.authProfile.userProfile;
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  logOut(): void {
    this.authService.logout()._finally(() => {
      this.authProfile.resetProfile();
      this.router.navigateByUrl('/login');
    }).subscribe(
      response => {
      },
      error => {
      }, () => {
      });

  }
}
