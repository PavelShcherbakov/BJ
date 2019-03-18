import { NgModule } from '@angular/core';

import { CreateGameComponent } from './create-game/create-game.component';
import { GameComponent } from './game.component';
import { SharedModule } from '../shared/shared.module';
import { TableComponent } from './table/table.component';
import { BotHandComponent } from './bot-hand/bot-hand.component';
import { UserHandComponent } from './user-hand/user-hand.component';
import { GameRoutingModule } from './game-routing.module';
import { CardComponent } from './card/card.component';

@NgModule({
  declarations: [CreateGameComponent, GameComponent, TableComponent, BotHandComponent, UserHandComponent, CardComponent],
  imports: [
    GameRoutingModule,
    SharedModule,
    GameRoutingModule
  ]
})
export class GameModule { }
