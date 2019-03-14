import { Component, OnInit } from '@angular/core';
import { AccountDataService } from '../../shared/services/account/account-data.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../shared/services/account/auth.service';
import { LoginAccountView } from '../../shared/entities/account.views/login.account.view';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { ConfirmRegistrationModalComponent } from '../confirm-registration-modal/confirm-registration-modal.component';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [AccountDataService]
})
export class LoginComponent implements OnInit {

  constructor(
    private dataService: AccountDataService,
    private router: Router,
    private authService: AuthService,
    private fb: FormBuilder,
    private modalService: BsModalService
  ) { }

  bsModalRef: BsModalRef;
  private userNames: string[];
  loginForm: FormGroup;

  ngOnInit() {

    this.loadUserNames();
    this.loginForm = this.fb.group({
      email: ['2WE3qwe@mail.com', Validators.required],
      password: ['2WE3qwe@mail.com', [Validators.required, Validators.minLength(6)]]
    });

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
      this.authService.login(loginView).subscribe(
        data => {
          this.router.navigate(['/game/create']);
        },
        (err) => {
          this.loginForm.controls.password.setErrors({ 'incorrect': true });
        });
      return;
    }

    const initialState = {
      loginView: loginView
    };
    this.bsModalRef = this.modalService.show(ConfirmRegistrationModalComponent, { initialState });
  }
}


