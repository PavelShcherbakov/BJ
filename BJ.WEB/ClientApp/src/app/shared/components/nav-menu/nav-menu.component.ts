import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ConfirmLogoutModalComponent } from '../confirm-logout-modal/confirm-logout-modal.component';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {

  constructor(private modalService: BsModalService) { }

  bsModalRef: BsModalRef;

  openModalWithComponent() {
    this.bsModalRef = this.modalService.show(ConfirmLogoutModalComponent);
  }
}
