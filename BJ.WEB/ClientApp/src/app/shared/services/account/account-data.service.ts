import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { GenericResponseView } from '../../entities/generic-response.view';
import { GetAllUserResponseAccountView } from '../../entities/account.views/get-all-user-response.account.view';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Config } from '../../configure/config';

@Injectable({
  providedIn: 'root'
})
export class AccountDataService {

  constructor(private http: HttpClient) { }

  public getUserNames(): Observable<GetAllUserResponseAccountView> {
    return this.http.get<GenericResponseView<GetAllUserResponseAccountView>>(Config.baseUrl + '/Account/GetAllUsers')
      .pipe(
        map(
          data => {
            const model: GetAllUserResponseAccountView = data.model;
            return model;
          }),
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }
}
