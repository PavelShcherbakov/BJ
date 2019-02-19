import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { CreateGameComponent } from './game/create-game/create-game.component';
import{GameModule}from './game/game.module';
import { from } from 'rxjs';
 

const appRoutes: Routes = [
  {path:"",component:LoginComponent},
  {path:"create-game",component:CreateGameComponent}
];

@NgModule({
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    GameModule
  ],

  exports: [RouterModule]
})
export class AppRoutingModule { }
