import { Component, OnInit } from '@angular/core';
import { GameDataService } from 'src/app/shared/services/game/game-data.service';
import { TableModel } from 'src/app/shared/models/table.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements OnInit {

  constructor(private dataService: GameDataService, private router: Router) { }
  private model: TableModel = new TableModel();

  public ngOnInit(): void {
    this.dataService.getState().subscribe(x => this.model = x, err => this.router.navigate(['/game/create']));
  }

  private hit(): void {
    this.dataService.getCard().subscribe(x => this.model = x);
  }

  private save(): void {
    this.dataService.endGame().subscribe(x => this.model = x);
  }

  private goToGame(): void {
    this.router.navigate(['/history/game', this.model.gameId]);
  }
}
