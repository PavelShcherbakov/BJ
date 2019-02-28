import { HasActiveGameGameResponseView } from './../../entities/game.views/has-active-game.game.view';
import { GetCardResponseGameView } from './../../entities/game.views/get-card-response.game.view';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StartGameResponseView } from '../../entities/game.views/start-response.game.view';
import { GenericResponseView } from '../../entities/generic-response.view';
import { Config } from '../../configure/config';
import { map } from 'rxjs/operators';
import { StartGameView } from '../../entities/game.views/start.game.view';
import { GetStateResponseGameView } from '../../entities/game.views/get-state-response.game.view';
import { EndResponseGameView } from '../../entities/game.views/end-response.game.view';

@Injectable({
  providedIn: 'root'
})
export class GameDataService {

  constructor(private http: HttpClient) { }

  public startGame(startGameView: StartGameView): Observable<StartGameResponseView> {
    return this.http.post<GenericResponseView<StartGameResponseView>>(Config.baseUrl + '/Game/Start', startGameView).pipe(map(data => {
      const model: StartGameResponseView = data.model;
      return model;
    }));

  }

  public getState(): Observable<GetStateResponseGameView> {
    return this.http.get<GenericResponseView<GetStateResponseGameView>>(Config.baseUrl + '/Game/GetState').pipe(map(data => {
      const model: GetStateResponseGameView = data.model;
      return model;
    }));
  }

  public getCard(): Observable<GetCardResponseGameView> {
    return this.http.post<GenericResponseView<GetCardResponseGameView>>(Config.baseUrl + '/Game/GetCard', {}).pipe(map(data => {
      const model: GetCardResponseGameView = data.model;
      return model;
    }));
  }

  public endGame(): Observable<EndResponseGameView> {
    return this.http.post<GenericResponseView<EndResponseGameView>>(Config.baseUrl + '/Game/End', {}).pipe(map(data => {
      const model: EndResponseGameView = data.model;
      return model;
    }));
  }

  public hasActiveGame(): Observable<HasActiveGameGameResponseView> {
    return this.http.get<GenericResponseView<HasActiveGameGameResponseView>>(Config.baseUrl + '/Game/HasActiveGame').pipe(map(data => {
      const model: HasActiveGameGameResponseView = data.model;
      return model;
    }));
  }

}
