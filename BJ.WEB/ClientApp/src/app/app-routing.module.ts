import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import{GameModule}from './game/game.module';
 

const appRoutes: Routes = [
  {path:"",loadChildren: './account/account.module#AccountModule'},
  {path:"game",loadChildren:'./game/game.module#GameModule'},
  {path:"history",loadChildren:'./history/history.module#HistoryModule'}
];

@NgModule({
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes, { preloadingStrategy:PreloadAllModules}),
    GameModule
  ],

  exports: [RouterModule]
})
export class AppRoutingModule { }
