import { OnlyLoginGuard } from './shared/guards/only-login.guard';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { OnlyLogoutGuard } from './shared/guards/only-logout.guard';


const appRoutes: Routes = [
  { path: '', redirectTo: '/account/login', pathMatch: 'full' },

  { path: 'account', canActivate: [OnlyLogoutGuard], loadChildren: './account/account.module#AccountModule' },
  { path: 'game', canActivate: [OnlyLoginGuard], loadChildren: './game/game.module#GameModule' },
  { path: 'history', canActivate: [OnlyLoginGuard], loadChildren: './history/history.module#HistoryModule' },
  { path: '**', redirectTo: '/account/login' }
];

@NgModule({
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes, { preloadingStrategy: PreloadAllModules })
  ],
  providers: [
    OnlyLoginGuard,
    OnlyLogoutGuard
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
