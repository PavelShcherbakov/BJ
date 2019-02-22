import { Component, OnInit } from '@angular/core';
import { AccountDataService } from '../../shared/services/account/account-data.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from "@angular/router";
import { AuthService } from "../../shared/services/account/auth.service";
import { LoginAccountView } from "../../shared/entities/account.views/login.account.view";


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [AccountDataService]
})
export class LoginComponent implements OnInit {

  constructor(private dataService: AccountDataService, private router: Router, private authService: AuthService, private fb: FormBuilder) {
  }

  private userNames: string[];
  loginForm: FormGroup;

  ngOnInit() {

    // загрузка данных при старте компонента  
    this.loadUserNames();
    this.loginForm = this.fb.group({
      email: ["", Validators.required],
      password: ["", [Validators.required, Validators.minLength(6)]]
    });
    this.loginForm.valueChanges.subscribe((value) => console.log(value));
    this.loginForm.statusChanges.subscribe((status) => {
      console.log(this.loginForm.errors);
      console.log(status);

    })

  }

  isControlInvalid(controlName: string): boolean {
    const control = this.loginForm.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  loadUserNames() {

    this.dataService.getUserNames().subscribe(data => {
      this.userNames = data.userNames;
    });

  }

  onSubmit() {

    let loginView = new LoginAccountView();
    loginView = { ...this.loginForm.value };


    const isExistUser = this.userNames.findIndex(x => x === loginView.email);
    if (isExistUser > -1) {
      this.authService.login(loginView).subscribe(data => {
        this.router.navigate(["/game/create"]);

      }, (err) => {
        alert(err.message);
      });
      return;
    }


    this.authService.register(loginView).subscribe(data => {
      this.router.navigate(["/game/create"]);
      debugger;
    }, (err) => {

      alert(err.message);
    });

    console.log(this.loginForm.value);
  }
}
