import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { IUser } from 'src/app/core/interfaces/user.interface';
import { urls } from 'src/app/core/extension/urls';
import { getTokenValue } from 'src/app/core/helpers';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentUserSubject: BehaviorSubject<IUser>;
  public currentUser: Observable<IUser>;
  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<IUser>(getTokenValue());
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue() {
    return this.currentUserSubject.value;
  }

  login(form): Observable<any> {
    return this.http.post<any>(urls.POST_SIGN_IN(), form.value)
    .pipe(map(user => {
      if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          const userValue = getTokenValue();
          this.currentUserSubject.next(userValue);
      }
      return user;
    }));
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  getToken() {
    return localStorage.getItem('currentUser');
  }

  registration(form) {
    return this.http.post(urls.POST_SIGN_UP(), form);
  }

  changePassword(form) {
    return this.http.put(urls.PUT_CHANGE_PASSWORD(), form);
  }
}
