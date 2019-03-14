import { Injectable } from '@angular/core';
import { tokenNotExpired } from 'jwt-decode';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { LoginAccountView } from '../../entities/account.views/login.account.view';
import { LoginResponseAccountView } from '../../entities/account.views/login-response.account.view';
import { GenericResponseView } from '../../entities/generic-response.view';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Config } from '../../configure/config';
import { RegisterAccountView } from '../../entities/account.views/register.account.view';

@Injectable()
export class AuthService {

  constructor(private router: Router, private http: HttpClient) { }

  public getToken(): string {
    return localStorage.getItem('accesToken');
  }

  public isAuthenticated(): boolean {
    const token = this.getToken();
    return tokenNotExpired(token);
  }

  public async login1(model: LoginAccountView): Promise<LoginResponseAccountView> {

    const result = await this.http.post<GenericResponseView<LoginResponseAccountView>>(Config.baseUrl + '/Account/Login', model)
      .toPromise();
    return result.model;
  }

  public login(loginView: LoginAccountView): Observable<LoginResponseAccountView> {
    return this.http.post<GenericResponseView<LoginResponseAccountView>>(Config.baseUrl + '/Account/Login', loginView)
      .pipe(
        map(
          data => {
            const token = data.model.token;
            localStorage.setItem('accesToken', token);
            const model: LoginResponseAccountView = data.model;
            return model;
          }),
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })

      );
  }

  public register(registerView: RegisterAccountView): Observable<LoginResponseAccountView> {
    return this.http.post<GenericResponseView<LoginResponseAccountView>>(Config.baseUrl + '/Account/Register', registerView)
      .pipe(
        map(
          data => {
            const token = data.model.token;
            localStorage.setItem('accesToken', token);
            const model: LoginResponseAccountView = data.model;
            return model;
          }),
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }

  isAuth(): boolean {
    if (localStorage.getItem('accesToken') === null) {
      return false;
    }
    return true;
  }

  logout() {
    localStorage.removeItem('accesToken');
  }
}
