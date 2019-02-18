import { Component, OnInit } from '@angular/core';
import { AccountDataService } from '../account-data.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [AccountDataService]
})
export class LoginComponent implements OnInit {

  constructor(private dataService: AccountDataService) { }

  userNames: string[];

  ngOnInit() {
    this.loadUserNames();    // загрузка данных при старте компонента  
  }

  loadUserNames() {
    this.dataService.getUserNames().subscribe((data) => this.userNames = data["model"]["userNameList"]);
  }

}
