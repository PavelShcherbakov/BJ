import { Game } from './../../shared/models/dashboard.model';
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

  get games(): Game[] {
    return this.model.games
      .map((game, i) => ({ id: i + 1, ...game }))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  model: Dashboard = new Dashboard();
  page = 1;
  pageSize = 5;
  collectionSize = 0;

  ngOnInit() {
    this.dataService.getAllGames().subscribe(
      x => {
        this.model.games = this.sortGamesByData(x.games);
        this.collectionSize = x.games.length;
      });
  }

  sortGamesByData(games: Game[]): Game[] {
    return games.sort((a, b) => new Date(b.creationDate).getTime() - new Date(a.creationDate).getTime());
  }

  goToGame(gameId: Guid) {
    this.router.navigate(['/history/game', gameId]);
  }
}
