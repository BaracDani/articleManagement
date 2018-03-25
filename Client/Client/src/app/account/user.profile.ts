import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {IProfile} from './user.model'

@Injectable()
export class UserProfile {
  userProfile: IProfile = {
    token: "",
    expiration: "",
    currentUser: { id: '', userName: '', email: '' },
    claims: null
  };
  constructor(private router: Router) {
  }

  setProfile(profile: any): void {
    //var nameid = profile.claims.filter(p => p.type == 'nameid')[0].value;
    //var userName = profile.claims.filter(p => p.type == 'sub')[0].value;
    //var email = profile.claims.filter(p => p.type == 'email')[0].value;
    sessionStorage.setItem('access_token', profile.access_token);
    sessionStorage.setItem('expires_in', profile[".expires"]);
    //sessionStorage.setItem('nameid', nameid);
    sessionStorage.setItem('userName', profile.userName);
    //sessionStorage.setItem('email', email);
  }

  getProfile(authorizationOnly: boolean = false): IProfile {
    var accessToken = sessionStorage.getItem('access_token');

    if (accessToken) {
      this.userProfile.token = accessToken;
      this.userProfile.expiration = sessionStorage.getItem('expires_in');
      if (this.userProfile.currentUser == null) {
        this.userProfile.currentUser = { id: '', userName: '', email: '' }
      }
      this.userProfile.currentUser.id = sessionStorage.getItem('nameid');
      this.userProfile.currentUser.userName = sessionStorage.getItem('userName');
    }

    return this.userProfile;
  }

  resetProfile(): IProfile {
    sessionStorage.removeItem('access_token');
    sessionStorage.removeItem('expires_in');
    this.userProfile = {
      token: "",
      expiration: "",
      currentUser: null,
      claims: null
    };
    return this.userProfile;
  }
}
