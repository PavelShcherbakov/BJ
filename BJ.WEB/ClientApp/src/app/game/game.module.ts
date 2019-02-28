import { WithoutActiveGamesGuard } from './../shared/guards/without-active-games.guard';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateGameComponent } from './create-game/create-game.component';
import { RouterModule, Routes } from '@angular/router';
import { GameComponent } from './game.component';
import { SharedModule } from '../shared/shared.module';
import { TableComponent } from './table/table.component';
import { BotHandComponent } from './bot-hand/bot-hand.component';
import { UserHandComponent } from './user-hand/user-hand.component';

// import { BrowserModule } from '@angular/platform-browser';

const routes: Routes = [

  {
    path: '', component: GameComponent, children: [
      { path: 'create', canActivate: [WithoutActiveGamesGuard], component: CreateGameComponent },
      { path: 'table', component: TableComponent }
    ]
  }

];

@NgModule({
  declarations: [CreateGameComponent, GameComponent, TableComponent, BotHandComponent, UserHandComponent],
  imports: [
    RouterModule.forChild(routes),
    SharedModule
  ],
  providers: [
    WithoutActiveGamesGuard
  ],
  exports: [
    RouterModule
  ]
})
export class GameModule { }
