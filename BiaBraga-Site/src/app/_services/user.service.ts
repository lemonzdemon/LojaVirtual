import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/User';
import { UrlAPI } from '../_utils/UrlAPI';

@Injectable({
  providedIn: 'root'
})
export class UserService {

constructor(private http: HttpClient) { }

getUserAuth(): Observable<User> {
  return this.http.get<User>(`${UrlAPI.UrlUser}auth`);
}

}
