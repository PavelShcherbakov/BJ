import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {GenericResponseView} from "../../entities/generic-response.view";
import {GetAllUserAccountResponseView} from "../../entities/account.views/get-all-user-response.account.view";
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Config } from '../../configure/config';

@Injectable({
  providedIn: 'root'
})
export class AccountDataService {

  constructor(private http: HttpClient) { }

  public getUserNames(): Observable<GetAllUserAccountResponseView> {
    return this.http.get<GenericResponseView<GetAllUserAccountResponseView>>(Config.baseUrl + "/Account/GetAllUsers").pipe(map(data=>{
      let model:GetAllUserAccountResponseView = data.model;
      return model}));
    
  }
}
