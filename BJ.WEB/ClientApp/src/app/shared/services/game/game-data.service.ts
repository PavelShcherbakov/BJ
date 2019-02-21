import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StartGameResponseView } from '../../entities/game.views/start-response.game.view';
import { GenericResponseView } from '../../entities/generic-response.view';
import { Config } from '../../configure/config';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class GameDataService {

  constructor(private http: HttpClient) { }

  public startGame(StartGameView): Observable<StartGameResponseView> {
    return this.http.post<GenericResponseView<StartGameResponseView>>(Config.baseUrl + "/Game/Start",StartGameView).pipe(map(data=>{
      let model:StartGameResponseView = data.model;
      return model}));
    
  }

}
