import { AuthService } from '../services/account/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanLoad, Route, UrlSegment, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable()
export class OnlyLoginGuard implements CanActivate {


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    boolean |
    import('@angular/router').UrlTree |
    Observable<boolean |
    import('@angular/router').UrlTree> |
    Promise<boolean | import('@angular/router').UrlTree> {

      if (this.authService.isAuth()) {
      return true;
    }
    this.router.navigate(['/account/login']);

  }

  constructor(private authService: AuthService, private router: Router) { }
}
