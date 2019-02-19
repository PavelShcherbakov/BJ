import { Component, OnInit } from '@angular/core';
import { AccountDataService } from '../account-data.service';
import { NgForm } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [AccountDataService]
})
export class LoginComponent implements OnInit {

  constructor(private dataService: AccountDataService, private router: Router, private http: HttpClient) { }

  private url = "/Account/Login";
  invalidLogin: boolean;
  userNames: string[];

  ngOnInit() {
    this.loadUserNames();    // загрузка данных при старте компонента  
  }

  loadUserNames() {
    this.dataService.getUserNames().subscribe((data) => this.userNames = data["model"]["userNameList"]);
  }

  login(form: NgForm) {
    let credentials = JSON.stringify(form.value);
    this.http.post(this.url, credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      let token = (<any>response["model"]).token;
      localStorage.setItem("jwt", token);
      this.invalidLogin = false;
      this.router.navigate(["/create-game"]);
    }, err => {
      this.invalidLogin = true;
    });
  }
}
