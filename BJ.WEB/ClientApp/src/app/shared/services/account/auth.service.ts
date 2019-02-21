import { Injectable } from '@angular/core';
import { tokenNotExpired } from 'jwt-decode';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { LoginAccountView } from '../../entities/account.views/login.account.view';
import { LoginAccountResponseView } from '../../entities/account.views/login-response.account.view';
import { GenericResponseView } from "../../entities/generic-response.view";
import { Observable } from 'rxjs';
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
    // get the token
    const token = this.getToken();
    // return a boolean reflecting 
    // whether or not the token is expired
    return tokenNotExpired(token);
  }

  public async login1(model: LoginAccountView): Promise<LoginAccountResponseView> {
    var result = await this.http.post<GenericResponseView<LoginAccountResponseView>>(Config.baseUrl + "/Account/Login", model).toPromise();
    return result.model;
  }

  public login(loginView: LoginAccountView): Observable<LoginAccountResponseView> {
    // let credentials = JSON.stringify(loginView);
    debugger
    return this.http.post<GenericResponseView<LoginAccountResponseView>>(Config.baseUrl + "/Account/Login", loginView).pipe(map(data => {
      debugger
      let token = data.model.token;
      localStorage.setItem("accesToken", token);
      let model: LoginAccountResponseView = data.model;
      return model
    }), catchError((err: HttpErrorResponse) => {
      return Observable.throw(err);
    })
    );
  }

  public register(registerView: RegisterAccountView): Observable<LoginAccountResponseView> {
    // let credentials = JSON.stringify(registerView);
    debugger
    return this.http.post<GenericResponseView<LoginAccountResponseView>>(Config.baseUrl + "/Account/Register", registerView).pipe(map(data => {
      debugger
      let token = data.model.token;
      localStorage.setItem("accesToken", token);
      let model: LoginAccountResponseView = data.model;
      return model
    }), catchError((err: HttpErrorResponse) => {
      return Observable.throw(err);
    })
    );
  }

}