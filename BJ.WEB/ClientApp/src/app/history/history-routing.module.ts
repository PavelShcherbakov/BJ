import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { GameInfoComponent } from './game-info/game-info.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HistoryComponent } from './history.component';

const routes: Routes = [
    {
        path: '', component: HistoryComponent, children: [
            { path: '', component: DashboardComponent },
            { path: 'game/:id', component: GameInfoComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class HistoryRoutingModule { }

