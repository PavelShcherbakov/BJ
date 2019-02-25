import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AccountModule } from './account/account.module';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthService } from './shared/services/account/auth.service';
import { TokenInterceptor } from './shared/interceptors/token.interceptor'
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorInterceptor } from './shared/interceptors/error.interceptor';
import { SharedModule } from './shared/shared.module';
import { OnlyLoginGuard } from './shared/guards/only-login.guard';
import { OnlyLogoutGuard } from './shared/guards/only-logout.guard';




@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule
  ],
  providers: [
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
    //     AuthGuard,
    // LogoutGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
