import { GameDataService } from 'src/app/shared/services/game/game-data.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanLoad, Route, UrlSegment, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { map, first } from 'rxjs/operators';

@Injectable()
export class WithoutActiveGamesGuard implements CanActivate {


    // canActivate(
    //     route: ActivatedRouteSnapshot,
    //     state: RouterStateSnapshot
    // ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    // }




    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    boolean |
    import('@angular/router').UrlTree |
    Observable<boolean |
        import('@angular/router').UrlTree> |
    Promise<boolean | import('@angular/router').UrlTree> {

    return this.gameDataService.hasActiveGame().pipe(
        map(x => {

            if (x.hasActiveGame) {

                this.router.navigate(['/game/table']);
                return false;
            } else {

                return true;
            }

        }), first());

}

    constructor(private gameDataService: GameDataService, private router: Router) { }
}

