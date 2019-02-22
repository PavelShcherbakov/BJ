import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account.component';
import { SharedModule } from '../shared/shared.module';

const routes: Routes = [

  { path: '', component: LoginComponent},
  { path: 'login', component: LoginComponent}

];


@NgModule({
  declarations: [LoginComponent, AccountComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    SharedModule
  ],
  exports: [LoginComponent]
})
export class AccountModule { }
