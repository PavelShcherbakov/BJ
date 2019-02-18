import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AccountDataService {

  private url = "/Account/GetAllUsers";
  constructor(private http: HttpClient) { }

  getUserNames() {
    return this.http.get(this.url);
  }
}
