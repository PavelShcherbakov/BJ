import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StartGameResponseView } from '../../entities/game.views/start-response.game.view';
import { GenericResponseView } from '../../entities/generic-response.view';
import { Config } from '../../configure/config';
import { map } from 'rxjs/operators';
import { StartGameView } from '../../entities/game.views/start.game.view';

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

}
