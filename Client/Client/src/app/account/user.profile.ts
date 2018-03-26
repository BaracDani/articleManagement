import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {IProfile} from './user.model'

@Injectable()
export class UserProfileService {
  userProfile: IProfile = {
    token: "",
    expiration: "",
    currentUser: { id: '', userName: '', email: '' },
    claims: null,
    roles: null
  };
  constructor(private router: Router) {
  }

  setProfile(profile: any): void {
    //var nameid = profile.claims.filter(p => p.type == 'nameid')[0].value;
    //var userName = profile.claims.filter(p => p.type == 'sub')[0].value;
    //var email = profile.claims.filter(p => p.type == 'email')[0].value;
    sessionStorage.setItem('access_token', profile.access_token);
    sessionStorage.setItem('expires_in', profile[".expires"]);
    sessionStorage.setItem('userName', profile.userName);
    sessionStorage.setItem('roles', JSON.stringify(profile.roles));
    //sessionStorage.setItem('email', email);
    //sessionStorage.setItem('nameid', nameid);
  }

  getProfile(authorizationOnly: boolean = false): IProfile {
    let accessToken = sessionStorage.getItem('access_token');

    if (accessToken) {
      this.userProfile.token = accessToken;
      this.userProfile.expiration = sessionStorage.getItem('expires_in');
      if (this.userProfile.currentUser == null) {
        this.userProfile.currentUser = { id: '', userName: '', email: '' }
      }
      //this.userProfile.currentUser.id = sessionStorage.getItem('nameid');
      this.userProfile.currentUser.userName = sessionStorage.getItem('userName');
      this.userProfile.roles = JSON.parse(sessionStorage.getItem('roles'));
    }

    return this.userProfile;
  }

  resetProfile(): IProfile {
    sessionStorage.removeItem('access_token');
    sessionStorage.removeItem('expires_in');
    sessionStorage.removeItem('userName');
    sessionStorage.removeItem('roles');
    this.userProfile = {
      token: "",
      expiration: "",
      currentUser: null,
      claims: null,
      roles: null
    };
    return this.userProfile;
  }
}
