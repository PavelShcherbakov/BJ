import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Config } from '../../configure/config';
import { map } from 'rxjs/operators';
import { GetAllGamesResponseHistoryView } from '../../entities/history.views/get-all-games-response.history.view';
import { GenericResponseView } from '../../entities/generic-response.view';
import { GetGameInfoHistoryResponseView } from '../../entities/history.views/get-game-info-response.history.view';
import { GetGameInfoHistoryView } from '../../entities/history.views/get-game-nfo.history.view';

@Injectable({
  providedIn: 'root'
})
export class HistoryDataService {

  constructor(private http: HttpClient) { }

  public getAllGames(): Observable<GetAllGamesResponseHistoryView> {
    return this.http.get<GenericResponseView<GetAllGamesResponseHistoryView>>(Config.baseUrl + '/History/GetAllGames').pipe(map(data => {
      const model: GetAllGamesResponseHistoryView = data.model;
      return model;
    }));
  }

  public getGameInfo(getGameInfoHistoryView: GetGameInfoHistoryView): Observable<GetGameInfoHistoryResponseView> {
    // tslint:disable-next-line: max-line-length
    return this.http.post<GenericResponseView<GetGameInfoHistoryResponseView>>(Config.baseUrl + '/History/GetGameInfo', getGameInfoHistoryView).pipe(map(data => {
      const model: GetGameInfoHistoryResponseView = data.model;
      return model;
    }));
  }

}
