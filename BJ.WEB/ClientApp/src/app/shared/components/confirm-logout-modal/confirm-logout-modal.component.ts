import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/account/auth.service';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-confirm-logout-modal',
  templateUrl: './confirm-logout-modal.component.html',
  styleUrls: ['./confirm-logout-modal.component.scss']
})
export class ConfirmLogoutModalComponent {

  constructor(private authService: AuthService, private router: Router, public bsModalRef: BsModalRef) { }

  confirm(): void {
    this.authService.logout();
    this.router.navigate(['/account/login']);
    this.bsModalRef.hide();
  }

  decline(): void {
    this.bsModalRef.hide();
  }

}
