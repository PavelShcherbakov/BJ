import { environment } from './../../../../environments/environment.prod';
import { HasActiveGameResponseGameView } from '../../entities/game.views/has-active-game-response.game.view';
import { GetCardResponseGameView } from './../../entities/game.views/get-card-response.game.view';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { StartResponseGameView } from '../../entities/game.views/start-response.game.view';
import { GenericResponseView } from '../../entities/generic-response.view';
import { map, catchError } from 'rxjs/operators';
import { StartGameView } from '../../entities/game.views/start.game.view';
import { GetStateResponseGameView } from '../../entities/game.views/get-state-response.game.view';
import { EndResponseGameView } from '../../entities/game.views/end-response.game.view';

@Injectable({
  providedIn: 'root'
})
export class GameDataService {

  constructor(private http: HttpClient) { }

  public startGame(startGameView: StartGameView): Observable<StartResponseGameView> {
    return this.http.post<GenericResponseView<StartResponseGameView>>(environment.apiUrl + '/Game/Start', startGameView)
      .pipe(
        map(
          data => {
            const model: StartResponseGameView = data.model;
            return model;
          }),
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }

  public getState(): Observable<GetStateResponseGameView> {
    return this.http.get<GenericResponseView<GetStateResponseGameView>>(environment.apiUrl + '/Game/GetState')
      .pipe(
        map(
          data => {
            const model: GetStateResponseGameView = data.model;
            return model;
          }),
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }

  public getCard(): Observable<GetCardResponseGameView> {
    return this.http.post<GenericResponseView<GetCardResponseGameView>>(environment.apiUrl + '/Game/GetCard', {})
      .pipe(
        map(
          data => {
            const model: GetCardResponseGameView = data.model;
            return model;
          }),
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }

  public endGame(): Observable<EndResponseGameView> {
    return this.http.post<GenericResponseView<EndResponseGameView>>(environment.apiUrl + '/Game/End', {})
      .pipe(
        map(
          data => {
            const model: EndResponseGameView = data.model;
            return model;
          }),
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }

  public hasActiveGame(): Observable<HasActiveGameResponseGameView> {
    return this.http.get<GenericResponseView<HasActiveGameResponseGameView>>(environment.apiUrl + '/Game/HasActiveGame')
      .pipe(
        map(
          data => {
            const model: HasActiveGameResponseGameView = data.model;
            return model;
          }),
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }
}
