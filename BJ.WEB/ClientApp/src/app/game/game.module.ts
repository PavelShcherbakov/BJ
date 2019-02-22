import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateGameComponent } from './create-game/create-game.component';
import { RouterModule, Routes } from '@angular/router';
import { GameComponent } from './game.component';
import { SharedModule } from '../shared/shared.module'
// import { BrowserModule } from '@angular/platform-browser';

const routes: Routes = [

  { path: 'create', component: GameComponent }

];

@NgModule({
  declarations: [CreateGameComponent, GameComponent],
  imports: [
    RouterModule.forChild(routes),
    SharedModule
  ],
  exports:[
    RouterModule
  ]
})
export class GameModule { }
