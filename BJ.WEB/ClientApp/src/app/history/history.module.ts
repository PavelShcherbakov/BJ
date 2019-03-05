import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HistoryComponent } from './history.component';
import { SharedModule } from '../shared/shared.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { GameInfoComponent } from './game-info/game-info.component';
import { SuitPipe } from '../shared/pipes/suit.pipe';

const routes: Routes = [
  {
    path: '', component: HistoryComponent, children: [
      { path: '', component: DashboardComponent },
      { path: 'game/:id', component: GameInfoComponent}
    ]
  }
];

@NgModule({
  declarations: [HistoryComponent, DashboardComponent, GameInfoComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ],
  exports: [
    RouterModule
  ]
})
export class HistoryModule { }
