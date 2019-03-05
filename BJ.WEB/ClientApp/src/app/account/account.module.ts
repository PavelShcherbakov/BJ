import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account.component';
import { SharedModule } from '../shared/shared.module';
import { ModalModule, AlertModule } from 'ngx-bootstrap';
import { ConfirmRegistrationModalComponent } from './confirm-registration-modal/confirm-registration-modal.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  {
    path: '', component: AccountComponent, children: [
      { path: 'login', component: LoginComponent }
    ]
  },
];


@NgModule({
  declarations: [LoginComponent, AccountComponent, ConfirmRegistrationModalComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    SharedModule,
    ModalModule.forRoot(),
    AlertModule.forRoot()
  ],
  entryComponents: [
    ConfirmRegistrationModalComponent
  ],
  exports: [LoginComponent]
})
export class AccountModule { }
