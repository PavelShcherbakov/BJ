import { Guid } from 'guid-typescript';
import { Component, OnInit } from '@angular/core';
import { HistoryDataService } from 'src/app/shared/services/history/history-data.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { GetGameInfoHistoryView } from 'src/app/shared/entities/history.views/get-game-nfo.history.view';
import { GameInfo } from 'src/app/shared/models/game-info';

@Component({
  selector: 'app-game-info',
  templateUrl: './game-info.component.html',
  styleUrls: ['./game-info.component.scss']
})
export class GameInfoComponent implements OnInit {

  private subscription: Subscription;
  private id: Guid;
  private model: GameInfo = new GameInfo();

  constructor(private dataService: HistoryDataService, private router: Router, private activateRoute: ActivatedRoute) {
    this.subscription = activateRoute.params.subscribe(params => this.id = params['id']);
  }



  ngOnInit() {

    const request: GetGameInfoHistoryView = new GetGameInfoHistoryView();
    request.gameId = this.id;
    this.dataService.getGameInfo(request).subscribe(x => this.model = x);
  }

}
