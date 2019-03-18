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

  private model: Dashboard = new Dashboard();
  private page = 1;
  private pageSize = 5;
  private collectionSize = 0;

  public ngOnInit(): void {
    this.dataService.getAllGames().subscribe(
      x => {
        this.model.games = this.sortGamesByData(x.games);
        this.collectionSize = x.games.length;
      });
  }

  private sortGamesByData(games: Game[]): Game[] {
    return games.sort(
      (a, b) => {
        const result = new Date(b.creationDate).getTime() - new Date(a.creationDate).getTime();
        return result;
      }
    );
  }

  private goToGame(gameId: Guid): void {
    this.router.navigate(['/history/game', gameId]);
  }
}
