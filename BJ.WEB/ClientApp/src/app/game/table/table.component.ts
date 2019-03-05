import { Component, OnInit } from '@angular/core';
import { GameDataService } from 'src/app/shared/services/game/game-data.service';
import { TableModel } from 'src/app/shared/models/table.model';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements OnInit {

  constructor(private dataService: GameDataService, private router: Router) { }
  model: TableModel = new TableModel();

  ngOnInit() {
    this.dataService.getState().subscribe(x => this.model = x, err => this.router.navigate(['/game/create']));
  }

  hit() {
    this.dataService.getCard().subscribe(x => this.model = x);
  }

  save() {
    this.dataService.endGame().subscribe(x => this.model = x);
  }

  goToGame() {
    this.router.navigate(['/history/game', this.model.gameId]);
  }
}
