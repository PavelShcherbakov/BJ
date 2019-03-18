import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HistoryComponent } from './history.component';
import { SharedModule } from '../shared/shared.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { GameInfoComponent } from './game-info/game-info.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { HistoryRoutingModule } from './history-routing.module';


@NgModule({
  declarations: [HistoryComponent, DashboardComponent, GameInfoComponent],
  imports: [
    CommonModule,
    SharedModule,
    NgbModule,
    HistoryRoutingModule
  ]
})
export class HistoryModule { }
