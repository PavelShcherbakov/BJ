import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CreateGameComponent } from './create-game/create-game.component';
import { GameComponent } from './game.component';
import { TableComponent } from './table/table.component';
import { WithoutActiveGamesGuard } from './../shared/guards/without-active-games.guard';

const routes: Routes = [

  {
    path: '', component: GameComponent, children: [
      { path: 'create', canActivate: [WithoutActiveGamesGuard], component: CreateGameComponent },
      { path: 'table', component: TableComponent }
    ]
  }

];

@NgModule({
    imports: [
    RouterModule.forChild(routes),
  ],
  providers: [
    WithoutActiveGamesGuard
  ],
  exports: [
    RouterModule
  ]
})
export class GameRoutingModule { }
