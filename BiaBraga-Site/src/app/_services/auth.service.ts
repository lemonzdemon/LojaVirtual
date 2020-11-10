import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { KeysApp } from '../_utils/KeysApp';
import { UrlAPI } from '../_utils/UrlAPI';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  jwtHelper = new JwtHelperService();
  decodedToken: any;

constructor(private http: HttpClient) { }

login(model: any) {
  return this.http.post(`${UrlAPI.UrlUser}login`, model).pipe(
    map((response: any) => {
      const _user = response;
      if(_user) {
        localStorage.setItem(KeysApp.localStorageJWT, _user.token);
      }
    })
  );
}

register(model: any) {
  return this.http.post(`${UrlAPI.UrlUser}register`, model);
}

loggedIn() {
  const token = localStorage.getItem(KeysApp.localStorageJWT);
  return !this.jwtHelper.isTokenExpired(token);
}

}
