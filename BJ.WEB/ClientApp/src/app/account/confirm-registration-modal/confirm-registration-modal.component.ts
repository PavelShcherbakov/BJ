import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/shared/services/account/auth.service';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap';
import { LoginAccountView } from 'src/app/shared/entities/account.views/login.account.view';

@Component({
  selector: 'app-confirm-registration-modal',
  templateUrl: './confirm-registration-modal.component.html',
  styleUrls: ['./confirm-registration-modal.component.scss']
})
export class ConfirmRegistrationModalComponent {

  loginView: LoginAccountView;

  constructor(private authService: AuthService, private router: Router, public bsModalRef: BsModalRef) { }

  confirm(): void {
    this.authService.register(this.loginView).subscribe(data => {
      this.router.navigate(['/game/create']);

    }, (err) => {

      alert(err.message);
    });
    this.bsModalRef.hide();
  }

  decline(): void {
    this.bsModalRef.hide();
  }

}
