import { environment } from './../../../../environments/environment.prod';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { GetAllGamesResponseHistoryView } from '../../entities/history.views/get-all-games-response.history.view';
import { GenericResponseView } from '../../entities/generic-response.view';
import { GetGameInfoResponseHistoryView } from '../../entities/history.views/get-game-info-response.history.view';
import { GetGameInfoHistoryView } from '../../entities/history.views/get-game-info.history.view';

@Injectable({
  providedIn: 'root'
})
export class HistoryDataService {

  constructor(private http: HttpClient) { }

  public getAllGames(): Observable<GetAllGamesResponseHistoryView> {
    return this.http.get<GenericResponseView<GetAllGamesResponseHistoryView>>(environment.apiUrl + '/History/GetAllGames')
      .pipe(
        map(
          data => {
            const model: GetAllGamesResponseHistoryView = data.model;
            return model;
          }),
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }

  public getGameInfo(getGameInfoHistoryView: GetGameInfoHistoryView): Observable<GetGameInfoResponseHistoryView> {
    return this.http.post<GenericResponseView<GetGameInfoResponseHistoryView>>(environment.apiUrl + '/History/GetGameInfo', getGameInfoHistoryView)
      .pipe(
        map(
          data => {
            const model: GetGameInfoResponseHistoryView = data.model;
            return model;
          }),
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }
}
