import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AccountModule } from './account/account.module';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AuthService} from './shared/services/account/auth.service';
import { TokenInterceptor} from './shared/interceptors/token.interceptor'
import { HTTP_INTERCEPTORS } from '@angular/common/http';




@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AccountModule,
    HttpClientModule
  ],
  providers: [
    AuthService, {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
