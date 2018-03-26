import {Component, OnInit} from '@angular/core';
import {IProfile} from './account/user.model';
import {UserService} from './account/user.service';
import {UserProfile} from './account/user.profile';
import {Router} from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  pageTitle: string = 'Article Management app';
  loading: boolean = true;
  Profile: IProfile;

  constructor(private authService: UserService,
              private authProfile: UserProfile,
              private router: Router) {
  }

  ngOnInit(): void {
    this.Profile = this.authProfile.userProfile;
  }

  logOut(): void {
    this.authService.logout().subscribe(
      response => {
        this.authProfile.resetProfile();
        this.router.navigateByUrl('/login');
      },
      error => {
      });

  }
}
