import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountComponent } from './account.component';
import { SharedModule } from '../shared/shared.module';
import { ModalModule, AlertModule } from 'ngx-bootstrap';
import { ConfirmRegistrationModalComponent } from './confirm-registration-modal/confirm-registration-modal.component';
import { AccountRoutingModule } from './account-routing.module';

@NgModule({
  declarations: [LoginComponent, AccountComponent, ConfirmRegistrationModalComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    ModalModule.forRoot(),
    AlertModule.forRoot(),
    AccountRoutingModule
  ],
  entryComponents: [
    ConfirmRegistrationModalComponent
  ],
  exports: [LoginComponent]
})
export class AccountModule { }
