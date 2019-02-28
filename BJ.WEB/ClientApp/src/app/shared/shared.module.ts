import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap';
import { ConfirmLogoutModalComponent } from './components/confirm-logout-modal/confirm-logout-modal.component';

@NgModule({
  declarations: [NavMenuComponent, ConfirmLogoutModalComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    ModalModule.forRoot()
  ],
  entryComponents: [
    ConfirmLogoutModalComponent
  ],
  exports: [
    NavMenuComponent,
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
