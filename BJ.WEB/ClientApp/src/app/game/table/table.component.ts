import { Component, OnInit } from '@angular/core';
import { GameDataService } from 'src/app/shared/services/game/game-data.service';
import { GetStateResponseGameView } from 'src/app/shared/entities/game.views/get-state-response.game.view';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements OnInit {

  constructor(private dataService: GameDataService) { }
  model: GetStateResponseGameView;

  ngOnInit() {
    this.dataService.getState().subscribe(x => this.model = x);
  }
  hit() {
    this.dataService.getCard().subscribe(x => this.model = x);
  }

  save(){
    this.dataService.endGame().subscribe(x => this.model = x);
  }
}
