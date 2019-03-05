import { Guid } from 'guid-typescript';
import { HistoryDataService } from './../../shared/services/history/history-data.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Dashboard } from 'src/app/shared/models/dashboard.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  constructor(private dataService: HistoryDataService, private router: Router) { }

  model: Dashboard = new Dashboard();

  ngOnInit() {
    this.dataService.getAllGames().subscribe(x => this.model = x);
  }
  goToGame(gameId: Guid) {
    this.router.navigate(['/history/game', gameId]);
  }
}
